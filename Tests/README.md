# Metodología de test unitarios

Esta es nuestra propuesta para el desarrollo de test.  
Confluye en gran parte con las cosas vistas en el curso,
añadiendo ciertas modificaciones para aprovechar modismos de C#
o paliar la ausencia de otros de Java.

## Índice (ToC)

- [Resumen (TL;DR)](#resumen-tldr)
- [Motor de test: NUnit](#motor-de-test-nunit)
- [Estructura de los test: patrón "AAA"](#estructura-de-los-test-patrón-aaa)
- [Librerías usadas](#librerías-usadas)
  * [Dobles de test: NSubstitute](#dobles-de-test-nsubstitute)
    + [*Mocks*](#mocks)
    + [*Stubs*](#stubs)
  * [Asserts más legibles: FluentAssertions](#asserts-más-legibles-fluentassertions)
- [Nomenclatura de los test](#nomenclatura-de-los-test)
  * [Nombre de los test: BDD (no estricto)](#nombre-de-los-test-bdd-no-estricto)
  * [Nombre del contenido de los test](#nombre-del-contenido-de-los-test)
- [Encapsulaciones del *arrange*](#encapsulaciones-del-arrange)
  * [Creación de sut y docs: *Builders* y fachada estática](#creación-de-sut-y-docs-builders-y-fachada-estática)
    + [Clase base *Builder*](#clase-base-builder)
    + [*Builders* que heredan de *Builder*](#builders-que-heredan-de-builder)
    + [Interfaz fluida (*fluent API*)](#interfaz-fluida-fluent-api)
    + [Constructor de builders con visibilidad de paquete](#constructor-de-builders-con-visibilidad-de-paquete)
    + [Patrón *ObjectMother*](#patrón-objectmother)
    + [Fachada estática](#fachada-estática)
    + [Posibles problemas](#posibles-problemas)
  * [Creación de dobles: *MockBuilder* y fachada estática de *mocks*](#creación-de-dobles-mockbuilder-y-fachada-estática-de-mocks)
    + [*MockBuilders* que heredan de *Builder*](#mockbuilders-que-heredan-de-builder)
    + [Fachada estática de *mocks*](#fachada-estática-de-mocks)
- [Arquitectura de test+producción](#arquitectura-de-test-y-producción)
  * [Paquetes «amigos»](#paquetes-amigos)
  * [Diagrama de arquitectura](#diagrama-de-arquitectura)
- [Uso de test parametrizados y teorías](#uso-de-test-parametrizados-y-teorías)


##  Resumen (TL;DR)

TBC.

## Motor de test: NUnit

NUnit es la librería de testing más extendida y madura de .NET. Se basa en xUnit como casi todas, por
lo que las diferencias entre ella y JUnit son sutiles.  
A fin de comentar las alternativas, la herramienta [xUnit.net](https://github.com/xunit/xunit) fue escrita
por el creador de NUnit v2, forma parte de la [.NET Foundation](https://dotnetfoundation.org/)
y en la última década se ha convertido en una opción cada vez más escogida.

**Justificación**: Unity tiene un motor propio de testing basado en NUnit, por lo que no había decisión que tomar.  

Algunas notas para sintetizar someramente la sintaxis de NUnit:  
- `[Test]`: atributo que decora a cada método que representa un test unitario, que debe ser `public void`.
  - Nota: podría ser `public Task` en test asíncronos pero el NUnit de Unity no lo soporta aún y en esta práctica no nos hace falta.

- `[TestCase([params])]`: atributo que genera un caso para un test parametrizado pasándole los parámetros en *params*.

- `[Theory]`: atributo alternativo a `[Test]` para marcar que para todo valor posible de los parámetros de ese test, el test debe satisfacerse (se explican más adelante).

- `[TestFixture]`: atributo que decora a cada clase que contiene test.
  - Hace varias versiones de NUnit que no hace falta ponerlo (una clase con un `[Test]` ya lo es implícitamente).
  - Esta nomenclatura resulta confusa, ya que la *fixture* es el escenario que se crea para los test de esa clase. Es decir, el *arrange*.
    Pero es precisamente ese escenario común el que marca la cohesión de los mismos y por tanto la que da esencia a la clase de test.
  

## Estructura de los test: patrón "AAA"
Igual que hemos visto en clase, el formato de un test diferencia bien a simple vista tres partes:

- **Arrange**: crea y modifica los objetos necesarios para el test (sut, docs, etc.).
- **Act**: ejercita la unidad bajo prueba (casi siempre un método) y guarda, si es necesario, resultados.
- **Assert**: comprueba que la unidad bajo prueba ha satisfecho las expectativas.

Son tres **partes explícitas**. Ninguna de ellas se encapsula en otra parte de la clase de test,
a diferencia de lo que hemos visto en algunos casos en clase.  
**Justificación**: se prima la legibilidad individual de cada test
sobre la repetición de código que supone tener escenarios similares en los *arranges* de muchos test distintos.  
La única excepción son casos en los que realmente no existe alguna parte de las tres, como por ejemplo el *arrange*
a la hora de probar utilidades estáticas.

Estas tres partes se diferencian, a ser posible, con una simple línea en blanco.  
En caso de que eso dificulte la legibilidad de una de ellas se puede especificar con
comentarios el inicio de cada una de las tres partes (algo frecuente en la comunidad de C#).

No obstante, que una de esas tres partes sea poco legible es, probablemente, síntoma de un *smell code*.  
Por ejemplo, podría estar pidiendo a gritos encapsular las creaciones en nuevos *builders* (se verá más adelante).

Ejemplo de test sin comentarios:
```
[Test]
public void SomeTestMethod()
{
    var doc = new SomeDOC();
    var sut = new SomeEasyToTestSUT(doc);
    
    var result = sut.Act();
    
    result.Should().BeTrue();
}
```

Ejemplo de test con comentarios:
```
[Test]
public void SomeTestMethod()
{
    //Arrange
    var doc = new SomeDOC();
    var sut = new SomeEasyToTestSUT(doc);
    
    //Act
    var result = sut.Act();
    
    //Assert
    result.Should().BeTrue();
}
```
Las tres zonas de comentarios ayudan a comprender más rápidamente el test, pero, si se trata de 
algo tan legible como el primer caso, no aportan nada respecto a los saltos de línea.



## Librerías usadas

En la comunidad de C# no se ha dado aún el paso de java de fagocitar una librería para que los
asserts sean más legibles. Sin embargo, existen un par de librerías tan extendidas como las de
dobles de test, por lo que toda la comunidad usa alguna combinación de entre todas las posibles.


### Dobles de test: NSubstitute

Junto con MoQ, [NSubstitute](https://nsubstitute.github.io/help/getting-started/) copa el mercado de librerías de dobles de test disponibles en C#.

**Justificación**: optamos por NSubstitute porque integra de forma más natural con la siguiente herramienta,
y porque es la que más fácil soporta Unity (mediante una dll).

NSubstitute se basa en el método estático y genérico `Substitute.For<T>()` para crear dobles de test.

**Restricciones importantes**: solo interfaces y métodos/propiedades virtuales pueden *mockearse*. Como comentó
Luis, en un momento dado esta restricción también existía en java. En este caso la restricción continúa.

#### *Mocks*

Para definir expectativas, NSubstitute se basa en el método de extensión `.Received()`, sus sobrecargas y derivadas.  
Estas se escriben directamente como aserciones.

Ese método recibe *matchers* muy similares a los que hemos visto en Mockito,
esta vez para fijar bajo qué condiciones se han recibido esas llamadas. 

Ejemplos:
```
//¿El método SomeMethod ha recibido 1 sola llamada con expectedCallArguments como argumentos?
mock.Received(1).SomeMethod(expectedCallArguments);

//¿El método SomeMethod no ha recibido ni una sola llamada?
mock.DidNotReceive().SomeMethod();

//¿El método SomeMethod ha recibido alguna llamada con un entero como argumento?
mock.Received().SomeMethod(Arg.Any<int>());

//¿El método SomeMethod ha recibido alguna llamada con un entero negativo como argumento?
mock.Received().SomeMethod(Arg.Is<int>(x => x < 0));
```

#### *Stubs*

Como la mayoría de librerías de *mocks*, NSubstitute permite a su vez fingir entidades sobre la marcha.  
Es decir, crear *stubs* que se comporten como un sut espera en cierto test.

Para ello se basa esta vez en el método de extensión `.Returns()`, sus sobrecargas y derivadas.  
Estas se escriben como interfaz fluida (*fluent API*) encadenando otro método, al que «fingen».

Ejemplos:
```
//Devuelve n cuando se llama al método SomeMethod(), que no tiene parámetros.
mock.SomeMethod().Returns(n);

//Devuelve m cuando se llama al método SomeMethod() con el argumento n.
mock.SomeMethod(n).Returns(m); 
//Como el anterior, pero esta vez cuando se le llama con 1 y 2 como argumentos.
mock.SomeMethod(n1, n2).Returns(m);

//Como los anteriores, pero esta vez da igual con qué parámetros se le llame.
mock.SomeMethod(default).ReturnsForAnyArgs(m);
```

### Asserts más legibles: FluentAssertions

[FluentAssertions](https://fluentassertions.com/introduction) es una librería archiconocida en C#. Sigue la misma filosofía de patrón intérprete que el `Assert.That([value], [matcher statement])` visto en java.

Consigue unas aserciones mucho más legibles aún, gracias al uso de métodos de extensión y su combinación con interfaz fluida (de ahí su nombre: de ser una *fluent API*).  
Se desencadena a partir del método de extensión `.Should()`, el cual es enriquecido por gran cantidad de *matchers*.

Ejemplos:
```
result.Should().BeTrue();

result.Should().ContainValue(expected).Which.SomeProperty
      .Should().BeGreaterThan(minExpected);
      
result.Should().Throw<InvalidOperationException>();    
```

Las aserciones son por supuesto extensibles para dar a los test más semántica de nuestro propio dominio.

## Nomenclatura de los test

### Nombre de los test: BDD (no estricto)

BDD ofrece, entre otras cosas, una nomenclatura de test muy útil generalmente. Se basa en la estructura *Given_When_Then*,
evidentemente similar a las tres partes del patrón AAA visto antes. Pero BDD es mucho más y en esta práctica solo nos interesa
su nomenclatura de testing.  

**Justificación**: en equipos con insuficiente experiencia en testing, *Given_When_Then* ofrece tres pilares sobre los que edificar
tanto el nombre de un caso de test que no es fácil nombrar como el diseño de casos de test nuevos. Además, la comunidad de C# entre
otras muchas está abrazando cada vez más esta nomenclatura.

Personalmente, consideramos sin embargo que atarse a una única y rígida nomenclatura para nombrar los test puede ser contraproducente.
Hay muchísimos casos en los que otras nomenclaturas favorecen la lectura y comprensión de el test al que bautizan.
Por eso preferimos tener el *Given_When_Then* como base para unificar la nomenclatura estándar en el equipo, pero no se impone como si se tratase de una guía de estilo.

Por otro lado, explicitar las tres palabras clave no es necesario salvo que clarifiquen el propósito del test.

Ejemplos:
```
//Casos con Given_When_Then implícito
void Turn_SwitchDirection_DirectionChanges()
void Turn_SwitchDirectionTwice_DirectionIsTheSame()
void Draw_FromPlentyCards_DoesNotReturnAlwaysTheFirstInsertedCard()

//Casos con alguna de las palabras clave explícita
void AnyUnoGame_PlayAnyCard_ThenThatCardIsLastDiscard()

//Casos con alguna de las tres partes obviada
void PlayAnyCard_ThenThatCardIsLastDiscard()
void AnyColor_MatchesToAllOtherColors()

//Casos donde otra nomenclatura ayuda más
void NumeredCard_DoesNotMatchOther_IfColorIsNotTheSame_RegardlessTheyHaveSameNumber()
```

### Nombre del contenido de los test

Algunas recurrencias:
- Aquel objeto que contiene lo que va a ejercitar el test se nombra `sut` (práctica habitual en .NET).
- Por consecuencia, los docs del sut incluyen en su nombre `doc`.
- Los dobles de test incluyen como mínimo `mock` en su nombre.
  - Mejor si especifican qué tipo de doble son: `stub`, `spy`, `dummy`...
  - No llamar nunca `stub` a un *mock* ([referencia](https://martinfowler.com/articles/mocksArentStubs.html)).
- Si el test es muy fácil de leer, el resultado que se recoge en el *act* y se aserta después puede llamarse `result` o contener esa palabra en su nombre.
- Si se está probando una precondición que lanza una excepción, se hace mediante una función anónima recogida en una variable de nombre `act`.
- Si es de utilidad para la legibilidad del test, se recoge la expectativa en una variable que contiene `expected` en su nombre.
  
## Encapsulaciones del *arrange*

### Creación de sut y docs: *Builders* y fachada estática

Como se ha visto en clase, la encapsulación de los constructores en test apremia,
ya sea:  
- para no crear en todos los test dependencias hacia esos constructores, dejando a merced de la cirujía de escopetazos cualquier cambio en la API de esos constructores;
- para evitar que los *arrange* de los test se vuelvan gigantes y sucios;
- para, siguiendo con lo anterior, aumentar lo máximo posible la síntesis y claridad de las creaciones en *arrange*.

#### Clase base *Builder*

Una clase abstracta y genérica que:

1. Tiene un método abstracto *Build()* para forzar a quien la hereda a especificar cómo se crean objetos del tipo que crea *(T)*.

2. Especifica la conversión implícita al tipo que crea *(T)* para no tener que escribir el `.Build()` final.

Por ejemplo, `Deck sut = new DeckBuilder()` funcionaría porque *DeckBuilder* se convierte implícitamente a *Deck* mediante *Build()*. 

#### *Builders* que heredan de *Builder*

Clases que:

1. Crean objetos de un tipo *(T)* y heredan de la anterior (*`Builder<T>`*).

2. Tienen un atributo por cada parámetro de constructor del tipo que construyen *(T)*.
   
3. Inicializan esos atributos a un estado no de error que pasar a los constructores.

4. Tienen una interfaz fluida para cambiar esos atributos. Esto se explica más adelante.

5. Ponen su propio constructor con visibilidad de paquete. Esto se explica más adelante.

6. Tienen métodos estáticos que encapsulan las creaciones más socorridas del tipo que crean *(T)*. Esto se explica más adelante.

7. Implementan, por último, el método abstracto *Build()* llamando a tales constructores enviando sus propios atributos respectivamente.

#### Interfaz fluida (*fluent API*)

Cada builder tiene métodos que permiten la modificación fluida y semántica de sus atributos.  
Usualmente, estos métodos se nombran con el prefijo *With* seguido del atributo que modifican.  
En los casos de colecciones, se suele permitir pasar un número variable de argumentos gracias a 
la palabra reservada `params` (equivalente a `...` en Java).  
También en otros casos se suele ampliar esta API para hacerla aún más fácil de usar y de leer.  
Por ejemplo,  
```
UnoBuilder.WithTotalPlayers(n)    
          .WithHumanPlayers(m)    
          .WhereHumanPlayFirst()
```
Sería un builder del juego Uno para una partida de *n* jugadores, de los cuales *m* son humanos y su turno va antes que los que maneja la CPU. 

#### Constructor de builders con visibilidad de paquete

Los builders ocultan su propio constructor, al que dan visibilidad de paquete `internal` (más adelante se explica el porqué).
De esta manera, se fomenta que la encapsulación ganada por un lado al crear el builder no se pierda ahora creando el propio builder.
Como alternativa, cada builder permite crear una isntancia de sí mismo con un método estático sin parámetros llamado *New()*,
que simplemente llama a ese constructor oculto.  
Por ejemplo, `var sut = PlayerBuilder.New()` recibiría un PlayerBuilder ya creado.

#### Patrón *ObjectMother*

Este patrón fue [propuesto por Stephanie Punke y Peter Schuh](http://citeseerx.ist.psu.edu/viewdoc/download?doi=10.1.1.18.4710&rep=rep1&type=pdf)  
en la archiconocida consultora ThoughtWorks, de [Martin Fowler](https://martinfowler.com/bliki/ObjectMother.html).  
Alivia los *arrange* de los test y les aporta vocabulario de dominio.    

Son métodos similares al *New()* anterior que se encargan, simplemente, de encapsular la preparación de objetos recurrentes dentro de los casos de uso del proyecto.
Pueden o no ser estáticos dependiendo de su naturaleza y de lo que mejor case con su utilización en los test.

Algunos ejemplos:
- Un empleado que solo lleva una semana contratado (de cara a test que manejen contrataciones recientes).
- Un alumno temporal extranjero (de cara a test que manejen, por ejemplo, becas ERASMUS). 
- `CardBuilder.BunchOf(n)`, ya dentro de nuestro proyecto, que generaría una colección de *n* cartas directamente.

#### Fachada estática

Una práctica habitual, no solo de C#, es encapsular la propia creación de los builders.
En clase lo hemos visto en [la versión 3 del curso de testing](https://github.com/USantaTecla-3-pruebas/x-unitTests).
Pero en esa ocasión se hacían haciendo uso de una suerte de factorías.

Nosotros, más cerca de lo que hemos visto como tendencia en la comunidad de .NET, haremos uso de una fachada estática.

Esta fachada consta de:  
1. Una clase:
   - pública y estática;
   - llamada *Build*;
   - que acompaña a cada suite de test de la arquitectura.
2. Un método por cada builder:
   - público y estático;
   - llamado como la clase que crea ese builder *(T)*;
   - que devuelve el builder correspondiente creado mediante el anterior *New()*;
   - o bien el tipo que crea *(T)* si se trata de un ObjectMother estático.

De manera que la forma de crear, por ejemplo, un builder de comodines sería tan fácil como `var sut = Build.WildCard();`.
Y la forma de crear cartas verdes a través de la fachada estática de un builder de cartas, podría ser, por ejemplo, `var sut = Build.Card().WithColor(Red);`.

**Justificación**: al mismo tiempo se genera la riqueza y limpieza de la fachada estática, como vimos en clase,
sin perder la potencia de los builders, como sí discutimos en ese momento.

#### Posibles problemas

En ocasiones pueden darse conflictos de nombrado por tener varias fachadas estáticas llamadas todas *Build*.  
Si por ejemplo tenemos una clase Build en dominio como atajo para crear entidades y otra en aplicación para crear controladores,
C# podría mostrar un error en compilación por ambigüedad entre la *Build* de dominio y la *Build* de aplicación.  
O también podría pasar si estamos en una aplicación que hace uso del dominio de otra más general.
Por ejemplo, si el juego Uno usase un tablero y tuviésemos ya un paquete creado con todo lo referente a tableros en general,
la clase *Build* de tableros colisionaría con la nuestra.

Nuestra solución a este conflicto es usar alias que aporten la semántica suficiente como para no generar más confusión.
Se puede dejar como simplemente *Build* la que más vaya a usarse y especificarse aquella más esporádica.

Esto se haría mediante  
```  
using Build = [nombre_del_paquete].Build
```
a la hora de importar al inicio del archivo.

En el primero de los ejemplos anteriores, se podría dejar como *Build* la de controladores, ya que los test serían de esa capa, y bautizar como *DomainBuild* a la de dominio.
En el segundo, claramente podría renombrarse el de tablero como *BoardBuild* y dejar el del juego Uno como *Build* a secas.

Por otro lado, en otras ocasiones se da la redundancia de tener que empezar la creación por *Build* y terminarla por *Build()*.
Por ejemplo cuando no funciona correctamente la conversión implícita o cuando se quiere mantener el uso de `var`.

La solución a lo segundo es nunca usar `var` para el sut, algo que además se considera buena práctica en C# porque mantiene a primera vista
la noción de qué elemento de los creados es el que de verdad se está poniendo a prueba.
Acerca de la solución a lo primero... es más una cuestión de perfeccionismo estético que otra cosa, pero la misma solución puede aplicarse:
no usar `var` solventará el problema la mayoría de veces, salvo cuando ocurra por culpa del polimorfismo y su problema con la conversión implícita.  

En ocasiones hay gente que ha preferido, solo por este detalle, usar una palabra clave distinta para la fachada estática, sacrificando a nuestro parecer
cierta homogeneidad léxica.

Estos son los mayores impedimentos encontrados hasta la fecha en la metodología propuesta y, en caso de dar alguien con una solución mejor,
agradeceremos que se aporte como *issue* a este repositorio para poder mejorar. :)


>*Nota: el uso de clases parciales para la fachada estática queda descartado como una opción dado que
>aquellas que colisionen estarán siempre en ensamblajes (paquetes) distintos y por tanto no pueden compartir
>parcialidad, sino que serían dos clases distintas igualmente. :(*


### Creación de dobles: *MockBuilder* y fachada estática de *mocks*

Es, tras todo lo anterior, especialmente notable lo sucios que quedan aquellos test con mocks.
Aunque la librería NSubstitute dulcifica su creación y configuración, palidecen al lado de
los builders y su fachada estática.

Es por eso que se propone encaminar sobre la misma premisa el caso de los dobles de test.

#### *MockBuilders* que heredan de *Builder*

Al igual que en el caso anterior, cada doble que se necesite en test se crea de forma encapsulada,
mediante un builder que, esta vez:
1. El atributo que contiene es directamente del tipo que crea *(T)*. 
2. En su método *Build()* simplemente crea el doble de test con la librería asociada.

Puede intuirse que esto aliviaría levemente la cantidad de código a tocar en caso de cambiar la librería de dobles de test.
Los test seguirían muy acoplados porque, en principio, crean ellos las expectativas y suplantaciones que requieren.
Sin embargo ahora tienen encapsulada al menos la creación del doble en sí, por lo que sería mucho más fácil migrar usando adaptadores.

Aun así esta práctica sería muy costosa y es por eso que, siguiendo esta metodología, la librería de dobles sigue siendo una decisión muy
férrea y difícil de cambiar, como así lo es la de aserciones.

No obstante, ahora que queda encapsulado en un builder, pueden llevarse a Object Mothers aquellas suplantaciones recurrentes.

#### Fachada estática de *mocks*

Consideramos crucial no ocultar información en el test sobre si se está usando la clase real o suplantándose con dobles.
Es por eso que, si bien los mockbuilders podrían tunelarse también mediante la fachada estática *Build*, se propone
una fachada estática alternativa para tener consciencia a simple vista de cuándo se está creando un doble.

**Justificación**: entre otras cosas, como no acoplar la fachada estática de builders a la librería de dobles, destaca la posible
puesta en jaque de la capacidad de triangulación de los test, si quien los revisa no profundiza y da por hecho que un doble no lo era o viceversa.

Esta fachada paralela se ha denominado *Fake* y por lo demás es exactamente igual que la *Build* anterior.
Es más, estas dos palabras son mera azúcar sintáctica y siempre que mantengan cierta coherencia semántica se podrían usar cualesquiera.  
De hecho, hay muchas otras implementaciones de este tipo de fachadas, de la cual nos gustaría comentar una por lo terriblemente anecdótica que resultaba:  
Lo resolvían usando el artículo *A*, de manera que quedaba aún más prosaico: `var sut = A.Card().WithColor(Yellow)`.
Por desgracia, para los sustantivos que empezaban con sonido vocálico no eran capaces de cargar con la culpa de mantener el error gramatical, por lo que,
de manera desastrosa, duplicaban la clase para llamar a otra *An* y añadirle esos casos, como sería por ejemplo `var sut = An.Eagle().With...`.

## Arquitectura de test y producción

### Paquetes «amigos»
En Unity los ensamblajes (paquetes) de test son obligatoriamente distintos a los de producción.  
En C# y .NET en general, se reconoce igualmente separarlos como una buena práctica.
Es por eso que Microsoft ofrece la posibilidad de que un paquete marque a otros como «amigos» (por usar la metáfora de C++),
haciendo visible su API interna (que no privada; solo la que tenga visibilidad de paquete).

Para ello basta con agregar ciertos atributos decoradores que, por reflexión, se encarga luego Roslyn de coleccionar y modificar el ensamblaje (paquete) correspondiente.
A fin de tenerlos unificados y fácilmente accesible todos, el convenio es crear un fichero llamado `AssemblyInfo.cs` en el que solo se incluyen los medatados de ensamblaje.

Por ejemplo, un fichero sería tal que:
```
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: AssemblyCompany("CompanyName")]
[assembly: AssemblyProduct("ProductName")]
[assembly: AssemblyTitle("PackageName")]

[assembly: AssemblyVersion("0.0.1")]
[assembly: AssemblyCopyright("(C) 2021 CompanyName")]

//Aquí se marca el ensamblaje (paquete) de test como "amigo".
[assembly: InternalsVisibleTo("CompanyName.ProductName.Tests")]
//Aquí es donde se crean dinámicamente los dobles de test en NSubstitute.
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")] 
```

No hace falta puntualizar que esta práctica requiere una estricta y bien definida política de nombrado de paquetes.

### Diagrama de arquitectura

TBC.

## Uso de test parametrizados y teorías

Se usarán test parametrizados para hacer casos de test (`[TestCase]` en C#).  
Estos casos de test enviarán al test valores límite representativos de las diferentes clases de equivalencia.  
Si ayuda a la comprensión, un comentario puede acompañar a cada caso de test para referenciar esa clase de equivalencia.

Cuando hacer distintos casos de test de un mismo test desequilibre la legibilidad del mismo o disminuya la riqueza de su título,
se dividirá en varios test en lugar de usar uno solo con test parametrizados. 

Si los parámetros del test deberían cumplir el test para todos sus valores posibles, entonces estamos ante una teoría y el test
se marcará como tal usando el atributo `[Theory]` en lugar de `[Test]`.  
No hay que olvidar que si la teoría recibe parámetros de tipos discretos y finitos, el propio motor se encarga de hacerle los casos.
Por ejemplo, una teoría que recibe un *bool* o una *enum*, es recogida por NUnit para generar todos los casos distintos posibles.  
Para tipos no discretos ni finitos hace falta hacer un `[DataSource]`.

La combinación entre teorías y atributos *DataSource*, para terminar, son especialmente útiles para tener en una misma clase  
todas las pruebas de diferentes subtipos que tienen que satisfacer los test de su supertipo, en lugar de usar herencia en clase de test. 