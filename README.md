# Allure Viewer Portable (.NET 8 WinForms)

Mini herramienta portable para visualizar reportes Allure empaquetados en `.zip` en Windows 10/11 sin instalar nada extra.

## Funcionalidad
- Seleccionás un `.zip` con el reporte **ya generado** (contener `index.html` en raíz o dentro de `allure-report/`).
- La app descomprime en una carpeta temporal, levanta un mini servidor local (Kestrel) y abre el reporte en el navegador por defecto.
- Botón para **guardar log** si ocurre un error.
- **Limpieza automática** de archivos temporales al cerrar.

## Compilar (Visual Studio)
1. Abrí la solución (este proyecto).
2. Compilá en **Release x64**.
3. Publicá como **Single File** y **Self-contained** para `win-x64`:
   - Click derecho en el proyecto → *Publish* → *Folder*.
   - Target runtime: `win-x64`, Deployment mode: `Self-contained`, Produce single file: ✔.

## Compilar (CLI)
```bash
dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:SelfContained=true
```
El `.exe` queda en: `bin/Release/net8.0-windows/win-x64/publish/AllureViewerPortable.exe`

## Uso
1. Ejecutá `AllureViewerPortable.exe`.
2. Click **“Seleccionar…”** y elegí tu `.zip`.
3. Click **“Visualizar Reporte”**.
4. Click **“Cerrar”** para detener y limpiar.

> Nota: No genera reportes; **solo sirve** reportes ya compilados.
