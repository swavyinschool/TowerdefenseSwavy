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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        // Assign button references
        archer = root.Q<Button>("archer");
        sword = root.Q<Button>("sword");
        wizard = root.Q<Button>("wizard");
        upgrade = root.Q<Button>("upgrade");
        delete = root.Q<Button>("delete");

        // Attach click event listeners
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

        // Hide the menu initially
        root.visible = false;
    }

    private void CheckHideMenu(Vector3 clickPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(clickPosition, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("buildingPlaceGrass"))
        {
            // Show the menu if buildingPlaceGrass is clicked
            root.visible = true;
        }
        else
        {
            // Hide the menu if clicked elsewhere or no object is hit
            root.visible = false;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Determine the mouse click position in world space
            Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Check if the menu should be hidden based on the click position
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
        // Detach click event listeners
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
        selectedSite = site;

        if (selectedSite == null)
        {
            // Hide the menu if no site is selected
            root.visible = false;
            return;
        }

        // Show the menu and evaluate button states
        root.visible = true;
        EvaluateMenu();
    }

    public void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        // Disable all buttons by default
        archer.SetEnabled(false);
        sword.SetEnabled(false);
        wizard.SetEnabled(false);
        upgrade.SetEnabled(false);
        delete.SetEnabled(false);

        // Use a switch statement based on the site level to enable specific buttons
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
                delete.SetEnabled(true);
                break;
            default:
                break;
        }
    }
}
