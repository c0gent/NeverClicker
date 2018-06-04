# NeverClicker
#### [Download](https://github.com/nsan1129/NeverClicker/releases)

Automated invocation. May eat your laundry.


## Installation
1. Download the newest version of NeverClicker from [the 'Releases' page](https://github.com/nsan1129/NeverClicker/releases) (download the top NeverClicker_*.*.*.7z file).
2. [Download 7-zip](http://www.7-zip.org/download.html) and install it.
3. Unzip NeverClicker to any directory you want to permanently run it from: `C:\NeverClicker\`, `C:\Program Files\NeverClicker\`, whatever.
4. Make a shortcut for NeverClicker manually: open the folder you extracted to, right click `NeverClicker.exe` and click pin to start.
5. The options menu will open on your first run. Do what it says.
6. Ensure your configuration is correct by reading the following section and making any changes necessary.


## Configuration
- The game client should ideally be run in 'Windowed Maximized' mode at 1920x1080. If your resolution is different, see the [Screen Resolution](#screen-resolution) section below.
- Client **must** be running DirectX 9 for image detection to work (there is little reason to run DirectX 11 anyway).
- Keybinds can be customized in `AccountSettings.xml.txt`. Key modifiers follow the autohotkey conventions (Alt: !, Ctrl: ^, Shift: +) ex. Ctrl-i (invoke) would be `'^i'` in the settings file.
- **You must configure the settings configuration files manually** before activating Auto-Cycle (a friendly interface for this is coming eventually). You'll find a link to those files within the settings menu.
    - Set the number of characters on your account by editing the `<characterCount>` setting in the `<general>` section of `NeverClicker_GameAccount.ini`.
    - Enter your username and password into `AccountSettings.xml.txt`. If you're not comfortable with this you can manually log into the game client before starting the NeverClicker auto cycle.


## Professions
- Sort your profession assets in your inventory so that the optional assets you want to use are **above** all other assets. A rule of thumb is simply to sort all of your profession assets from lowest quality (top) to highest quality (bottom) in the bag.
- The profession system will first choose assets from the top of the bag to the bottom, then from the weakest asset (Mercenary) to the strongest (Hero). Remove any weak assets you don't want used from your inventory (or bury them somewhere at the bottom).
- Tasks are grouped together in the queue. For example, all "Guard Clerics" will be grouped together and the **slowest** one of the three tasks will be used for the queue timer. This means that it's pointless to have all Hero (purple) assets for two "Guard Clerics" tasks and a Footman and Mercenary for the third. In this case, the task group for "Guard Clerics" won't be re-processed until the footman and mercenary task is complete (unless another task happens to be processed in the interim). Try to even out your assets for each character, try to have 9 Mercenary & 9 Footman, or 9 Man-at-arms & 9 Hero, or 18 Adventurer or whatever. Be sure the weaker ones are sorted together at the top of the inventory as described above or else the tasks will be timed inefficently.


## Vault of Piety
- You can specify the desired Vault of Piety reward item by editing the `vaultOfPietyItem` setting for each character within `AccountSettings.xml.txt`. Valid options are:
  - ElixirOfFate = 0
  - BlessedProfessionsElementalPack = 1
  - CofferOfCelestialEnchantments = 2
  - CofferOfCelestialArtifacts = 3
  - CofferOfCelestialArtifactEquipment = 4 (default)


## Issues? Ideas?
Having a problem? Something doesn't work? Have a request for a new feature? [File an issue on the issues page](https://github.com/c0gent/NeverClicker/issues). In the case of problems, be sure to include:
- What you were doing / trying to do
- What you expected to happen
- What actually happened


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

- Other things to try:
	- Try running without anti-aliasing.
    - Try running NeverClicker as administrator.
    - Reset all settings to default. Rename or delete the `{Your Personal Folder}/Documents/NeverClicker` folder and the `user-config` settings file located somewhere in the `AppData` folder. You can find links to each of these locations within the settings menu in NeverClicker.


### Screen Resolution
Due to developer laziness, resolutions other than 1920x1080 will require some fiddling on your part to get working.

Mess with the following settings in `NeverClicker_GameClient.ini`:

*Note: An update to this section will be coming soon. Use ClientSettings.xml.txt instead and figure out the new settings names there.*

- The "[ClickLocations]" Section:
    - "CharSlotX" and "TopSlotY": You will need a program that can tell you your mouse cursor coordinates. I use the AutoIt Window Spy included with Autohotkey, downloaded from: (http://ahkscript.org/)[http://ahkscript.org/]. There are tons of other ones. Open your coordinate program, scroll to the top of the character select list, and place your cursor in the center of the top character tile (doesn't need to be exact). "CharSlotX" (the left->right component of your coordinate) should be somewhere in the 500-900ish range. "TopSlotY" (the top->bottom component) should be 60-150ish.

- The "[KeyBindAndUi]" Section:
    - "VisibleCharacterSelectSlots": The number of WHOLE slots you can see within the scrolling character select window when scrolled to the top. Just round down whatever number you see. If you see four slots plus almost all of a fifth but not quite, put "4".
    - "ScrollsToAlignBottomSlot": The number of scrolls it takes, when already scrolled to the very top, to cause the bottom most visible tile in the window to be fully visible. This might just always need to be "2" but there may be certain resolutions where it's different.
    - "CharacterSelectScrollBarTopX" and "CharacterSelectScrollBarTopY" (*Yes, I need to move these to the "ClickLocations" section but whatever*): These two must be exact. Find the little scrollbar to the right of the character select tile area. Scroll all the way up and place your mouse cursor on the very tip of the top of the scroll bar. This should be a spot you can click on no matter where the scroll bar actually is and it will cause the scroll bar to jump up towards the top. Same deal, the X position is "CharacterSelectScrollBarTopX", Y is "CharacterSelectScrollBarTopY".

Eventually, resolution auto-detection may set these things for you but for now you're on your own. Here are some known working settings so far:

- 1920x1080
    - `[KeyBindAndUi]`
        - `VisibleCharacterSelectSlots = 8`
        - `ScrollsToAlignBottomSlot = 2`
        - `CharacterSelectScrollBarTopX = 840`
        - `CharacterSelectScrollBarTopY = 86`
    - `[ClickLocations]`
        - `CharSlotX = 680`
        - `TopSlotY = 120`

- 1360x768
    - `[KeyBindAndUi]`
        - `VisibleCharacterSelectSlots = 4`
        - `ScrollsToAlignBottomSlot = 2`
        - `CharacterSelectScrollBarTopX = 587`
        - `CharacterSelectScrollBarTopY = 64`
    - `[ClickLocations]`
        - `CharSlotX = 435`
        - `TopSlotY = 100`

- 1280x1024
    - `[KeyBindAndUi]`
        - `VisibleCharacterSelectSlots = 6`
        - `ScrollsToAlignBottomSlot = 3`
        - `CharacterSelectScrollBarTopX = 551`
        - `CharacterSelectScrollBarTopY = 120`
    - `[ClickLocations]`
        - `CharSlotX = 407`
        - `TopSlotY = 151`