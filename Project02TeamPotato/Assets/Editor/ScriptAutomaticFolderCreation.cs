using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class ScriptAutomaticFolderCreation : MonoBehaviour {
	
	[MenuItem("Tool Creation/Create folder")]
	public static void CreateFolder()
	{
		AssetDatabase.CreateFolder("Assets", "Dynamic Assets");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets", "Resources");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animations");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Animations", "Sources");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Animation Controllers");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Effects");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Models");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Character");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Models", "Environment");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Prefabs");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Prefabs", "Common");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Sounds");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "Music");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/Music", "Common");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds", "SFX");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Sounds/SFX", "Common");
        AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "Textures");
		AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources", "UGC");
		AssetDatabase.CreateFolder("Assets/Dynamic Assets/Resources/Textures", "Common");
        AssetDatabase.CreateFolder("Assets", "Editor");
		AssetDatabase.CreateFolder("Assets", "Extensions");
		AssetDatabase.CreateFolder("Assets", "Gizmos");
		AssetDatabase.CreateFolder("Assets", "Plugins");
		AssetDatabase.CreateFolder("Assets", "Scripts");
        AssetDatabase.CreateFolder("Assets/Scripts", "Common");
        AssetDatabase.CreateFolder("Assets", "Shaders");
	    AssetDatabase.CreateFolder("Assets", "Static Assets");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Animations");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Animations", "Sources");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Animation Controllers");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Effects");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Models");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Models", "Character");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Models", "Environment");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Prefabs");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Prefabs", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Scenes");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Sounds");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Sounds", "Music");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Sounds/Music", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Sounds", "SFX");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Sounds/SFX", "Common");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources", "Textures");
        AssetDatabase.CreateFolder("Assets/Static Assets/Resources/Textures", "Common");
        AssetDatabase.CreateFolder("Assets", "Testing");

        AssetDatabase.Refresh();
	}
}
