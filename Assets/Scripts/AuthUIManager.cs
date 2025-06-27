using TMPro;
using UnityEngine;

public class AuthUIManager : MonoBehaviour
{
	public GameObject confirmPasswordInput;
	public GameObject loginButton;
	public GameObject registerButton;
	public GameObject switchToRegisterButton;
	public GameObject switchToLoginButton;
	public TMP_InputField usernameInput;
	public TMP_InputField passwordInput;

	void Start()
	{
		ShowLoginUI();
	}

	public void ShowRegisterUI()
	{
		if (usernameInput != null) usernameInput.text = "";
		if (passwordInput != null) passwordInput.text = "";
		if (confirmPasswordInput != null) confirmPasswordInput.name = "";
		confirmPasswordInput.SetActive(true);
		registerButton.SetActive(true);
		loginButton.SetActive(false);
		switchToRegisterButton.SetActive(false);
		switchToLoginButton.SetActive(true);
	}

	public void ShowLoginUI()
	{
		confirmPasswordInput.SetActive(false);
		registerButton.SetActive(false);
		loginButton.SetActive(true);
		switchToRegisterButton.SetActive(true);
		switchToLoginButton.SetActive(false);
	}
}