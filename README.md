# ObsidianPlugin
A Template Repository for building Obsidian plugins. Make sure to rename the assemblies to ensure compatibility with other plugins.

## Obsidian.Api.dll reference
You need the Obsidian.API dll to be able to develop plugins.
1. Add Obsidian.Api to your references.
2. Open your `.csproj` file.
3. Find the reference to `Obsidian.Api`.
4. Add the following node as a child to that property: `<Private>false</Private>`.

## Myget reference
Instead of directly referencing the DLL we can also pull it from myget for a quick streamlined dev environment. This template repository has the package reference set up for you, so you only need to add the package source.
1. Add `https://www.myget.org/F/obsidian/api/v3/index.json` to your Package sources.
2. Search for `Obsidian.Api` on Nuget.
3. Install the latest version.
4. Open your `.csproj` file.
5. Make sure the package reference looks like this:
```
<PackageReference Include="Obsidian.API" Version="1.0.110" ExcludeAssets="runtime">
  <Private>false</Private>
</PackageReference>
```
What's important here is that you include the `ExcludeAssets` parameter and the `Private` node. This will make sure Obsidian will use it's own Obsidian.Api assembly.

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