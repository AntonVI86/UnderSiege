using UnityEngine;
using UnityEngine.UI;

public class FriendAdder : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClickAddFriendButton);
    }

    private void OnClickAddFriendButton()
    {
        Agava.VKGames.SocialInteraction.InviteFriends();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClickAddFriendButton);
    }
}
