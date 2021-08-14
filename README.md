# Deceit Cosmetic Server
Deceit Cosmetic Server is a Party Server for Deceit written in C# for the purpose of viewing cosmetics. The server only works for one user and will never support multiple users.

Last tested version: ``202108111151`` (v2021.6.2b)

## Notes
- _You cannot use the cosmetics in the live version of the game_
- _Use this at your own risk. You are solely responsible for any action taken against your account._
- _I offer no support for this project. Do **not** ask for support in the official Deceit Discord_

## Usage
 - Download the latest [release](https://github.com/Kasuromi/DeceitCosmeticServer/releases) or compile the project. Check out [Compiling](#compiling) for required resources.
 - Locate your Deceit installation directory. It's usually located in `C:\Program Files (x86)\Steam\steamapps\common\Deceit\`
 - Open the `Assets` folder and open `Scripts.pak` with any ZIP reader. (7-Zip, WinRar, etc.)
 - Inside the PAK file, open the `Scripts` folder then `Unlocks`
 - Extract `unlocks.csv` to the DeceitCosmeticServer executable directory
 - Run the server. You should see `Listening on ws://127.0.0.1:9851`
 - Go back into your Deceit installation directory and open `system.cfg` with any text editor
 - Look for `de_partyServer=..`. Replace the line with `de_partyServer=ws://127.0.0.1:9851/party`
 - Save the file and launch Deceit through Steam
 - If everything went well, you should be in the Deceit Cosmetic Server

### Reverting
If you want to revert back to the original deceit version. Verify your game files through steam.

## Compiling
To compile the DeceitCosmeticServer project, you need:
- [Visual Studio 2019-2022](https://visualstudio.microsoft.com/vs/)
- [.NET 5.0 SDK](https://dotnet.microsoft.com/download)

## Links
- [Baseline Webiste](https://baseline.gg/)
- [Deceit Website](https://playdeceit.com/)
- [Deceit Discord](https://discord.gg/deceit)
- [GitHub](https://github.com/Kasuromi/DeceitCosmeticServer)