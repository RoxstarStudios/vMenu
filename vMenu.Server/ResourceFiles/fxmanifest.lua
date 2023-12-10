fx_version 'cerulean'
game 'gta5'
author 'Vespura'
description 'vMenu Revamped was created by XdGoldenTiger, Ricky, Katt, and the FiveM community'
version '1.0.0'

--
--	Set the language for vMenu.
--	See /languages for all known vMenu languages.
--	Only enter the name of the file, not including the file extension!
--	Yes: language 'en'
--	NO: language 'en.json'
--
language 'en'

client_script 'Client/vMenu.Client.net.dll'
server_script 'Server/vMenu.Server.net.dll'
shared_script 'Server/vMenu.Shared.net.dll'

files {	
    'Client/ScaleformUI.dll',	
    'Client/Newtonsoft.Json.dll',
    'Client/FxEvents.Client.dll',
    'Client/vMenu.Plugins.Client.dll',
    'Server/vMenu.Plugins.Server.dll',
    'Server/System.Memory.dll',
    'Server/System.Runtime.Loader.dll',
    'Server/System.Threading.Tasks.Extensions.dll',
    'Server/System.Runtime.CompilerServices.Unsafe.dll',
    'Server/System.Collections.Immutable.dll',
    'Server/Microsoft.CodeAnalysis.dll',
    'Server/Microsoft.CodeAnalysis.CSharp.dll',
    'stream/**/*',	
    'languages/*.json',	
    'config/*.jsonc',
    'Plugins/**/*'
}