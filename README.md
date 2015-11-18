# NeverClicker
Automated invocation. May eat your laundry.

## Installation
1. [Download the latest alpha](https://github.com/nsan1129/NeverClicker/releases) (download the latest NeverClicker_*.*.*.7z file).
2. [Download 7-zip](http://www.7-zip.org/download.html). Install 7-zip. Unzip NeverClicker to any directory you want to run it from. `C:\NeverClicker\`, `C:\Program Files\NeverClicker\`, whatever.
3. Run `NeverClicker.exe` (you may want to make a shortcut). The options menu will open on your first run. Do what it says.
4. Ensure your configuration is correct by reading the following section and making any changes necessary.

## Caveats
- Client should be run in 'Windowed Maximized' mode at 1920x1080.
- Client must be running DirectX 9 (there is little reason to run DirectX 11 anyway).
- Keybinds can be customized in `NeverClicker_GameAccount.ini`. Key modifiers follow the autohotkey conventions (Alt: !, Ctrl: ^, Shift: +) ex. Ctrl-i (invoke) would be `'^i'` in the ini file.  
- **You must configure the .ini configuration files manually** before activating Auto-Cycle (a friendly interface for this is coming eventually). You'll find a link to those files within the settings menu. 
  - You must enter your username and password into `NeverClicker_GameAccount.ini`. If you're not comfortable with this you can manually watch each time NeverClicker attempts to launch the patcher, wait for it's login attempt to fail, then type your username and password in manually. Things will continue normally after this point.
- You apparently shouldn't have a character on your account which is unable to invoke because they are too low level.

## Troubleshooting
NeverClicker is very much a work in progress. You will run in to problems. Please help us fix those.

- If there is a problem please do some troubleshooting:
  - Activate 'Log Debug Messages' in the options menu.
  - Consult the log file (default location is `C:\Users\{your windows user name}\Documents\NeverClicker\Logs`) to determine where the script is stuck.
  - 95% of the time it will be stuck on an image it cannot detect. This will be for one of two reasons:
    1. Your key bindings are not set up properly (see above).
	2. You need to customize the default images NeverClicker uses to detect screen elements. To modify these:
	  * Figure out where you're stuck (consult the log -- be sure 'Log Debug Messages' is enabled).
	  * Open the images folder (default location is `C:\Users\{your windows user name}\Documents\NeverClicker\Images`) and take a look at the corresponding image.
	  * Take an in-game screenshot (Ctrl-PrintScr) and crop it appropriately, making sure your image is the same size (or within a few pixels). Do not include extraneous parts of the image such as button borders, etc.
	3. Either overwrite your new cropped screenshot image over the default image file (be sure it's .png) or create an original file name and edit the game client ini and update the file name there.
  - If you still can't determine why you're stuck [please file an issue here] (https://github.com/c0gent/NeverClicker/issues) with the appropriate lines from your log file, settings files (be sure to erase your username and password!), and any other relevant information such as what was happening in-game.
    - Please follow these three simple universal rules for filing bug reports:
	  1. The complete steps to reproduce the bug: Relevant lines from your log file (not the whole thing, you need to do some pruning)
      2. The expected behavior: What were you expecting to see happen in the game client?
      3. The observed behavior: What actually happened. 99% of the time this answer will be 'nothing' or 'an error message'. Please include this information.
  
	
  

