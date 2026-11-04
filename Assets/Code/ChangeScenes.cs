using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
   public void InventoryScene()
    {
        SceneManager.LoadScene("Inventory Scene");
    }


    public void MainShop()
    {
        SceneManager.LoadScene("Main Scene");
    }
}
