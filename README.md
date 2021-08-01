# Estructura del proyecto.
```
|-- Runtime/  
   |-- Application/  
   |-- Domain/  
   |-- Infrastructure/  
|-- Tests/    
```

**Runtime**: Directorio que incluye el código de **producción**.   
**Application**: Directorio que incluye los **Controladores y vistas de entrada y salida al contexto del dominio**.  
**Domain**: Directorio que incluye los **modelos**.  
**Infrastructure**: Directorio que incluye **Vistas, Presentadores y Gateways**  
**Test**: Directorio que incluye el código de **pruebas**.  

[Documentación extensa sobre la arquitectura](https://github.com/USantaTecla-game-uno/requirements/tree/main/Docs/Architecture#correspondencia-con-mv)



# Estudio y decisión: Precondiciones.

Se decide optar por el uso de comprobaciones y lanzamiento de excepciones al inicio de los métodos como precondiciones.

```cs
public void foo() 
{
    if(precondition) 
    {
        throw new Exception();
    }
    
    // method body
}
```

## Porque no usamos Contracts de C#
1. Aunque funcionen, se evalúan y si pasan la condición funcionan bien. Pero si fallan parece ser que siempre lanzan un pop up con descripción de la excepción. Cosa que es un lío bastante gordo si estás ejecutando cientos de test forzando que las precondiciones fallen.

2. En Git la API fue abandonada por Microsoft y ofrecida a la comunidad, comunidad que después de una versión de C# en el que el compilador introdujo cambios, la API se vio afectada, se introdujeron bugs y parece ser que la comunidad lo "ha abandonado", el repo está archivado y no tiene actividad desde 2018. (más info aquí https://github.com/microsoft/CodeContracts/issues/409).

3. La Api está incluida en .net standard 2 y está ahí un poco como legacy code, pero hay bastante gente que dice que deberían quitarlo. Ya que es una abstracción con una sola implementación y que es lío de mantener, etc.

4. Hay una serie de propuestas abiertas con mejoras al diseño por contrato bastante chulas como esta: (https://github.com/dotnet/csharplang/issues/105) no sé si cuajaran, pero el hecho de que haya movimiento indica que puede que en versiones futuras de c# tengamos una mejor api para esta funcionalidad.

5. He visto gente de asp .net con problemas también tanto al compilar como casos de falsos positivos.

En general, la comunidad no esta contenta. Mencionan que les gusta el diseño por contrato y esta api tiene una buena legibilidad, pero en cuanto soporte, mantenimiento, estabilidad, etc. no es buena solución.

Alternativamente, mientras se desarrolla una mejor libreria hay gente que recomienda códigos como estos para tirar.

```
public class CustomContract
{
    public static void Requires<TException>( bool Predicate, string Message )
        where TException : Exception, new()
    {
       if ( !Predicate )
       {
          Debug.WriteLine( Message );
          throw new TException();
       }
    }
}  
```
o esto
```
public static class Contract
{
        public static void Requires<T>(bool result, string message) where T : Exception
        {
            if (!result)
            {
                T ex = (T)Activator.CreateInstance(typeof(T));
                typeof(T).GetField("_message", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(ex, message);
                throw ex;
            }
        }
}
```