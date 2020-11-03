# ObsidianPlugin
A Template Repository for building Obsidian plugins.

## Obsidian.API reference
You need the Obsidian.API dll to be able to develop plugins.
1. Add Obsidian.Api to your references.
2. Open your `.csproj` file.
3. Find the reference to `Obsidian.Api`.
4. Add the following node as a child to that property: `<Private>false</Private>`.

## Debugging Plugins
1. Go to your project properties.
2. Go to the Debug tab.
3. Set `Launch:` to Executable.
4. Click `Browse...` next to `Executable:` and find Obsidian.exe.
5. Click `Browse...` next to `Working Directory:` and select the folder that `Obsidian.exe` was in.
5. Go to the `Build` tab.
6. Click `Browse...` after `Output Path`.
7. Select your `plugins` folder.
8. Click the debug button to start debugging your plugin. You can now easily set breakpoints.