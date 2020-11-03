# ObsidianPlugin
A Template Repository for building Obsidian plugins.

## Myget
To add the nuget source:

1. `Tools>Nuget Package Manager>Package Manager Settings>Package Sources`,
2. click the big `+`
3. click the new value that appeared
4. set the name to anything you like (e.g. `Obsidian`)
5. set the source to `https://www.myget.org/F/obsidian/api/v3/index.json`
6. click `Update`

Now you should be able to use Obsidian's MyGet feed.

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
