using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
    public int changeValue = 1;

    public AudioClip autioClip;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log($"{col.gameObject.name} enter");

        RubyController rubyController = col.GetComponent<RubyController>();
        if (rubyController == null)
        {
            return;
        }

        if (!rubyController.CanChange(changeValue))
        {
            return;
        }

        rubyController.ChangeHp(changeValue);
        if (autioClip)
        {
            rubyController.PlaySound(autioClip);
        }

        Destroy(gameObject);
    }
}