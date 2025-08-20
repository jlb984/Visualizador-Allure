# Allure Viewer Portable (.NET 8 WinForms)

Mini herramienta portable para visualizar reportes Allure empaquetados en `.zip` en Windows 10/11 sin instalar nada extra.

## Funcionalidad
- Seleccionás un `.zip` con el reporte **ya generado** (contener `index.html` en raíz o dentro de `allure-report/`).
- La app descomprime en una carpeta temporal, levanta un mini servidor local (Kestrel) y abre el reporte en el navegador por defecto.
- Botón para **guardar log** si ocurre un error.
- **Limpieza automática** de archivos temporales al cerrar.
- Soporta rutas largas, acentos y espacios en nombres de archivos.
- Asigna automáticamente un puerto libre en `127.0.0.1`.
- No requiere permisos de administrador ni instalación de .NET en la PC destino.

---

## Compilar (Visual Studio 2022)
1. Abrí la solución (este proyecto) en Visual Studio 2022 (Community o superior).
2. Cambiá la configuración a **Release** y plataforma **x64** (desde la barra de configuración o *Configuration Manager*).
3. Compilá en **Release x64** con **Build → Build Solution** para verificar que no hay errores.
4. Publicá como **Single File** y **Self-contained** para `win-x64`:
   - Click derecho en el proyecto → *Publish* → *Folder*.
   - Elegí la carpeta de salida.
   - En configuración del perfil:
     - **Target runtime:** `win-x64`
     - **Deployment mode:** `Self-contained`
     - **Produce single file:** ✔
     - **Trimming:** desactivado
   - Guardá y ejecutá **Publish**.
5. El `.exe` quedará en la carpeta elegida para publicación.

---

## Compilar (CLI / VS Code)
1. Instalar **.NET 8 SDK** desde https://dotnet.microsoft.com/en-us/download
2. Abrir una terminal en la carpeta del proyecto (donde está `AllureViewerPortable.csproj`).
3. Ejecutar:
   ```powershell
   dotnet publish .\AllureViewerPortable.csproj -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true

4. El `.exe` quedará en:

   ```
   bin/Release/net8.0-windows/win-x64/publish/AllureViewerPortable.exe
   ```

---

## Uso

1. Ejecutá `AllureViewerPortable.exe`.
2. Click **“Seleccionar…”** y elegí tu `.zip` con el reporte Allure **ya generado**.
3. Click **“Visualizar Reporte”** → se abrirá en el navegador predeterminado.
4. Click **“Cerrar”** para detener el servidor y limpiar la carpeta temporal.
5. Si ocurre un error, usar **“Guardar log…”** para descargar el registro de ejecución.

> **Nota:** No genera reportes; **solo sirve** reportes ya compilados que contengan `index.html` en raíz o dentro de `allure-report/`.

---

## Troubleshooting / Solución de problemas

### 1. El antivirus o SmartScreen bloquea la ejecución

* **Windows SmartScreen** puede mostrar:

  > "Windows protegió su PC"

  * Hacé clic en **Más información** y luego **Ejecutar de todas formas**.
* En entornos corporativos, pedí a IT que marque el `.exe` como seguro.
* Si el antivirus lo elimina, agregá la carpeta a exclusiones (previa validación con IT).

---

### 2. El `.zip` no es válido o no abre

* Asegurate de que contenga un **reporte Allure ya generado**:

  * Debe tener `index.html` en la raíz o en una carpeta `allure-report/`.
  * Si el `.zip` solo contiene `allure-results/`, primero generá el reporte con:

    ```bash
    allure generate allure-results --clean -o allure-report
    ```

    y luego comprimí la carpeta `allure-report`.

---

### 3. No abre el navegador automáticamente

* Copiá y pegá la URL que muestra la aplicación en cualquier navegador.
* Asegurate de que tu navegador por defecto esté bien configurado en Windows.

---

### 4. Error de permisos o acceso denegado

* El `.exe` no requiere privilegios de administrador, pero si la carpeta de trabajo tiene restricciones, movelo a un lugar accesible como `C:\Users\<tu_usuario>\Desktop`.

---

### 5. Conflictos de puerto

* La app elige un puerto libre automáticamente. Si otro proceso lo ocupa justo después, reiniciá la app.

---

### 6. Guardar logs para soporte

* Si falla algo, usá el botón **“Guardar log…”**.
* Enviá el archivo `.txt` resultante al equipo de soporte para diagnóstico.

---

## Recomendaciones

* Usar siempre la **última versión** de la app.
* Mantener el `.zip` de reporte con nombres cortos y sin caracteres especiales extremos para evitar problemas en sistemas con restricciones de ruta.
* Verificar que el `.zip` no supere 500 MB (para evitar demoras en descompresión).

---