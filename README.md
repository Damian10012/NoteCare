# NoteCare
 C# App taking care of your note links in Obsidian

## How does it work
- The app starts in system tray with the Obsidian icon
- Clicking the icon brings the app to the foreground
	- Clicking inside the form thows it back to the tray
- It gets every Directory listed
	- Creates a root of each directory if not present
	- Writes every single file as a link into the root file
		- This is the way it creates so nice Graph view
			- Also suitable with a little changes for Mind Map generations