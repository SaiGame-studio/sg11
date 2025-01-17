using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SaiMonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance => instance;

    [Header("Page Management")]
    public List<UIPage> pages;
    public int currentPage = 0;
    public int defaultPage = 0;

    protected override void Awake()
    {
        base.Awake();
        if (UIManager.instance != null) Debug.LogError("Only 1 GridManagerCtrl allow to exist");
        UIManager.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadUIPages();
    }

    private void LoadUIPages()
    {
        if (this.pages.Count > 0) return;
        this.pages = new List<UIPage>(transform.GetComponentsInChildren<UIPage>());
        Debug.LogWarning(transform.name + " LoadUIPages", gameObject);
    }

    public void GoToPage(int pageIndex)
    {
        if (pageIndex < pages.Count && pages[pageIndex] != null)
        {
            SetActiveAllPages(false);
            pages[pageIndex].gameObject.SetActive(true);
        }
    }

    public void GoToPageByName(string pageName)
    {
        UIPage page = pages.Find(item => item.name == pageName);
        int pageIndex = pages.IndexOf(page);
        GoToPage(pageIndex);
    }

    public void SetActiveAllPages(bool activated)
    {
        if (pages != null)
        {
            foreach (UIPage page in pages)
            {
                if (page != null)
                    page.gameObject.SetActive(activated);
            }
        }
    }
}
