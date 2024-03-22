using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archer;
    private Button sword;
    private Button wizard;
    private Button upgrade;
    private Button delete;

    private ConstructionSite selectedSite;

    private VisualElement root;
    public static TowerMenu Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("xxxx");
        root = GetComponent<UIDocument>().rootVisualElement;

        archer = root.Q<Button>("archer");
        sword = root.Q<Button>("sword");
        wizard = root.Q<Button>("wizard");
        upgrade = root.Q<Button>("upgrade");
        delete = root.Q<Button>("delete");

        if (archer != null)
            archer.clicked += OnArcherButtonClicked;

        if (sword != null)
            sword.clicked += OnSwordButtonClicked;

        if (wizard != null)
            wizard.clicked += OnWizardButtonClicked;

        if (upgrade != null)
            upgrade.clicked += OnUpdateButtonClicked;

        if (delete != null)
            delete.clicked += OnDestroyButtonClicked;

        root.visible = false;
    }
    private void CheckHideMenu(Vector3 clickPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Hit object tag: " + hit.collider.tag); // Debug log the tag of the hit object
            if (hit.collider.CompareTag("buildingPlaceGrass"))
            {
                // Toon het menu als er op buildingPlaceGrass is geklikt
                root.visible = true;
                return; // Exit the method after showing the menu
            }
        }

        // Verberg het menu als er op een andere plaats is geklikt of als er geen object is geraakt
        root.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Bepaal de positie van de muisklik in de wereldruimte
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Controleer of het menu moet worden verborgen op basis van de klikpositie
            CheckHideMenu(clickPosition);
        }
    }

    private void OnArcherButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Archer, SiteLevel.Onbebouwd);
    }

    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Sword, SiteLevel.Onbebouwd);
    }

    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(TowerType.Wizard, SiteLevel.Onbebouwd);
    }

    private void OnUpdateButtonClicked()
    {
        // Implement update functionality
    }

    private void OnDestroyButtonClicked()
    {
        // Implement delete functionality
    }

    private void OnDestroy()
    {
        if (archer != null)
            archer.clicked -= OnArcherButtonClicked;

        if (sword != null)
            sword.clicked -= OnSwordButtonClicked;

        if (wizard != null)
            wizard.clicked -= OnWizardButtonClicked;

        if (upgrade != null)
            upgrade.clicked -= OnUpdateButtonClicked;

        if (delete != null)
            delete.clicked -= OnDestroyButtonClicked;
    }

    public void SetSite(ConstructionSite site)
    {
        Debug.Log("fffff");

        selectedSite = site;
        if (selectedSite != null)
        {
            EvaluateMenu();
            root.visible = true; // Show the TowerMenu when a site is selected
        }
        else
        {
            root.visible = false; // Hide the TowerMenu when no site is selected
        }
    }

    public void EvaluateMenu()
    {
        Debug.Log("GGDG");
        // Return if selectedSite equals null
        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }


        // Use the SetEnabled() function on every button
        archer.SetEnabled(true);
        sword.SetEnabled(true);
        wizard.SetEnabled(true);
        upgrade.SetEnabled(true);
        delete.SetEnabled(true);

        // If the site level for the selectedSite is zero, only the archerButton, wizardButton, and swordButton should be enabled.
        // If the site level is 1 or 2, only the update and destroyButton should work.
        // If the siteLevel is 3, only the destroyButton is enabled.

        switch (selectedSite.Level)
        {
            case SiteLevel.Onbebouwd:
                upgrade.SetEnabled(true);
                break;
            case SiteLevel.Level1:
            case SiteLevel.Level2:
                archer.SetEnabled(true);
                sword.SetEnabled(true);
                wizard.SetEnabled(true);
                break;
            case SiteLevel.Level3:
                archer.SetEnabled(true);
                sword.SetEnabled(true);
                wizard.SetEnabled(true);
                upgrade.SetEnabled(false);
                break;
            default:
                break;
        }
    }

}
