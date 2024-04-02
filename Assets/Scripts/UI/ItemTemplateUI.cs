using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTemplateUI : MonoBehaviour
{
    [SerializeField] private Image weaponImage;
    [SerializeField] private Image magazineImage;
    [SerializeField] private TextMeshProUGUI magazineCount;
    [SerializeField] private TextMeshProUGUI bulletCount;
    [SerializeField] private Button button;

    public Image GetImageComponent() {
        return weaponImage;
    }
    public Image GetMagImageComponent() {
        return magazineImage;
    }
    public TextMeshProUGUI GetMagCountComponent() {
        return magazineCount;
    }
    public TextMeshProUGUI GetBulletCountComponent() {
        return bulletCount;
    }

    public Button GetItemButton() {
        return button;
    }
}
