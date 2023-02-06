## API
- [ ]  Paquete completo: presentation, persistence...
- [-] 2-layered package: dominio e infraestructura.

- [ ] Los dominios no deberían apuntar a UnityEngine (no engine references = true).

- [ ] Añadir botones que solo crean partes concretas.
  - Ejemplo: botón para añadir la de editor. 

### AssemblyInfo
  - [ ]  Marca también a la de Castle para el proxy de mocks.

## Regresiones
- [ ] Test en la de Two-layered tienen una referencia vacía cada una al runtime madre, que no existe.
  - Esto ocurre porque por defecto apuntan a ese runtime y no deberían, es algo concreto de solo algunas.
- [ ] IMPORTANTE: la layout Min crea los test de Runtime y deberían ser de editor por defecto.
## Refactorización
- [ ]  NO Usar Path.Combine y demás.
  - [X]  En la concatenación de folderpaths.
  - [ ]  Se descarta porque genera mezcla de separadores ('/' y '\\').
- [ ]  Repo de carpetas de editor (guarda asmdefs y subcarpetas).
- [ ]  Tipo "cadena de cadenas" con separador, join y demás.
  - [ ]  StringSequence : IEnumerable
  - [ ]  Separator
  - [ ]  ToString() -> hace el Join.
  - [ ]  ctor -> hace el split.
  - [ ]  WitSeparator() -> devuelve otro con el nuevo separador.

### AssemblyInfo
- [ ]  Se usa SemVer en lugar de System.Version directamente.

## Invariantes

### Package layout

### Package layout folder
- [ ]  Cualquier hija añadida me tiene que tener a mí como madre.
- [ ]  '/' no es un carácter válido en el nombre.
- [x]  Los hijos no pueden compartir nombre.
- [ ]  Referencias cíclicas.