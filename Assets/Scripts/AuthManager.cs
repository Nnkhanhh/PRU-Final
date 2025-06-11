using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
	public TMP_InputField usernameInput;
	public TMP_InputField passwordInput;
	public TMP_Text messageText;

	public void OnRegister()
	{
		string username = usernameInput.text;
		string password = passwordInput.text;

		if (PlayerPrefs.HasKey(username + "_password"))
		{
			messageText.text = "Username đã tồn tại!";
			return;
		}

		PlayerPrefs.SetString(username + "_password", password);
		PlayerPrefs.SetInt(username + "_level", 1);
		PlayerPrefs.SetString("current_user", username);
		PlayerPrefs.Save();

		messageText.text = "Đăng ký thành công!";
		SceneManager.LoadScene("Map1");
	}

	public void OnLogin()
	{
		string username = usernameInput.text;
		string password = passwordInput.text;

		if (!PlayerPrefs.HasKey(username + "_password"))
		{
			messageText.text = "Username không tồn tại!";
			return;
		}

		string savedPass = PlayerPrefs.GetString(username + "_password");
		if (savedPass != password)
		{
			messageText.text = "Sai mật khẩu!";
			return;
		}

		PlayerPrefs.SetString("current_user", username);
		int level = PlayerPrefs.GetInt(username + "_level", 1);
		messageText.text = "Đăng nhập thành công!";
		SceneManager.LoadScene("Map" + level);
	}
}
