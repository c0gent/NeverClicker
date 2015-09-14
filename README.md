# NeverClicker
Automated invocation and leadership queueing. Currently in an alpha state. Will eat your laundry.

## Installation
1. [Download the latest alpha.](https://github.com/nsan1129/NeverClicker/releases/download/v0.2.2-alpha/NeverClicker_0.2.2.7z)
2. [Download 7zip](http://www.7-zip.org/download.html) and unzip to any directory you want to run it from. C:\NeverClicker\, C:\Program Files\NeverClicker\, whatever.
3. Run NeverClicker.exe (you might want to make a shortcut). The options menu will open on your first run. Do what it says.

## Caveats
- Client must run in 'Windowed Maximized' mode at 1920x1080.
- Client must be running DirectX 9 (there is little reason to run DirectX 11 anyway).
- Keyboard bindings (keybinds) must be configured in the game client for:
  - Invocation
  - Inventory
  - Professions
- Keybinds must be a single keyboard key (such as '9' or 'v') and **cannot** require modifiers (ALT, CTRL, etc.).
  - One method is to bind:
    - Invocation to '8'
	- Professions to '9'
	- and Inventory to '0'
  - Obviously, use whatever other *single-key* binding you prefer.
  - This keybind can be configured in addition to your present key binding in the game options so that you don't need to re-learn a new binding.
  
- **You must configure the .ini configuration files manually** before activating Auto-Cycle. Otherwise the keybinds and other settings will all be incorrect. You can access the files from within the settings menu for convenience. 
  - You must enter your username and password into the appropriate ini file. If you're not comfortable with this you can manually watch each time NeverClicker attempts to launch the patcher, wait for it's login attempt to fail, then type your username and password in manually. Things will continue normally after this point.

- Currently mercenaries are the only optional assets which are used in leadership tasks.
- You can't have characters on your account which are unable to invoke because they are too low level. Level the damn thing up for christ's sake.

## Troubleshooting
NeverClicker is very much a work in progress. You will run in to problems. Please help us fix those.

- If there are any problems you will need to do some troubleshooting:
  - Activate 'Log Debug Messages' in the options menu.
  - Consult the log file (default location is C:\Users\\*{your windows user name}*\Documents\NeverClicker\Logs) to determine where the script is stuck.
  - 95% of the time it will be stuck on an image it cannot detect. This will be for one of two reasons:
    1. Your key bindings are not set up properly (see above).
	2. You need to customize the default images NeverClicker uses to detect screen elements. To modify these:
	  * Figure out where you're stuck (consult the log -- be sure 'Log Debug Messages' is enabled).
	  * Open the images folder (default location is C:\Users\\*{your windows user name}*\Documents\NeverClicker\Images) and take a look at the corresponding image.
	  * Take an in-game screenshot (Ctrl-PrintScr) and crop it appropriately, making sure your image is the same size (or within a few pixels). Do not include extraneous parts of the image such as button borders, etc.
	3. Either overwrite your new cropped screenshot image over the default image file (be sure it's .png) or create an original file name and edit the game client ini and update the file name there.
  - If you still can't determine why you're stuck **please file an issue here on Github** with appropriate lines from your log file, settings files (be sure to erase your username and password!), and any other relevant information such as what was happening in-game.
    - Please follow these three simple universal rules for filing bug reports:
	  1. The complete steps to reproduce the bug: Relevant lines from your log file (not the whole thing, you need to do some work yourself)
      2. The expected behavior: What were you expecting to see happen in the game client?
      3. The observed behavior: What actually happened. 99% of the time this answer will be 'nothing' or 'an error message'. Please include this information.
  
	
  

