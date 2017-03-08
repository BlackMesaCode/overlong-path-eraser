# Overlong Path Eraser

Ever run into the problem that you cant delete a folder because it exceeds Windows' 256 character limit? *OverlongPathEraser* can help you:

1. Download the binaries [HERE](https://github.com/BlackMesaCode/overlong-path-eraser/releases/latest) and unzip them
2. Fire up your console (cmd or powershell)
3. Navigate into the downloaded folder
4. Execute OverlongPathEraser.exe and append the path to the folder you wish to delete: e.g. ```"OverlongPathEraser.exe D:\FolderToDelete"```

**NOTE**: *OverlongPathEraser* can only delete a single folder. No multiple folders at once, no files and no whole harddrives.


In case you have to delete overlong folders regularly I'd recommend you to add the downloaded folder to the global PATH variable. That way you can use *OverlongPathEraser* from everywhere within your filesystem.

GUI users can also drop a shortcut to the OverlongPathEraser.exe in Windows' SendTo Folder. The SendTo folder can be easily opened. Just use the hotkey ```Windows+R``` and enter ```shell:sendto```