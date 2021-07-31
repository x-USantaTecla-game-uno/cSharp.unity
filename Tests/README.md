# Metodología de test unitarios

Esta es nuestra propuesta para el desarrollo de test.  
Confluye en gran parte con las cosas vistas en el curso,
añadiendo ciertas modificaciones para aprovechar modismos de C#
o paliar la ausencia de otros de Java.

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

**Restricciones importantes**: solo interfaces y métodos/propiedades virtuales pueden *mockearse*. Como comentó
Luis, en un momento dado esta restricción también existía en java. En este caso la restricción continúa.

NSubstitute se basa en el método de extensión `.Received()`, sus sobrecargas y similares.  
Estas se escriben directamente como aserciones.

Ejemplos:
```
mock.Received(times(1)).SomeMethod(expectedCallArguments);

mock.DidNotReceive().SomeMethod();

mock.ReceivedForAnyArgs().SomeMethod();
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

## Uso de test parametrizados y teorías

Se usarán test parametrizados para hacer casos de test (`[TestCase]` en C#).  
Estos casos de test enviarán al test valores límite representativos de las diferentes clases de equivalencia.  
Si ayuda a la comprensión, un comentario puede acompañar a cada caso de test para referenciar esa clase de equivalencia.

Cuando hacer distintos casos de test de un mismo test desequilibre la legibilidad del mismo o disminuya la riqueza de su título,
se dividirá en varios test en lugar de usar uno solo con test parametrizados. 

Si los parámetros del test deberían cumplir el test para todos sus valores posibles, entonces estamos ante una teoría y el test
se marcará como tal usando el atributo `[Theory]` en lugar de `[Test]`.

## TBC

- Builders.
  - Visibilidad de paquete.
- Fachadas de los builders.
- Arquitectura general de los paquetes de test.
- ToC
