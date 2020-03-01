using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The class of assets that can be used to spawn in to the game.
/// Usage: GameAssests.i.{AssetRequired}
/// Example: GameAssets.i.PointsPopup
/// </summary>
public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if ( _i == null )
            {
                var sManager = GameObject.FindGameObjectWithTag("_SCENE_MANAGER_");
                if ( sManager == null)
                {
                    _i = Instantiate(Resources.Load("_SCENE_MANAGER_") as GameObject).GetComponent<GameAssets>();

                }
                else
                {
                    _i = sManager.GetComponent<GameAssets>();
                }

            }
            return _i;
        }
    }


    /// <summary>
    /// Move Point Indicator
    /// </summary>
    public Transform MovePointIndicator;
    public Transform PlayerArrowProjectile;
    public Transform PointsPopup;


    public Material[] EnemyMaterials;
}
