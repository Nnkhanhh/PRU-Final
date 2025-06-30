using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayFabLogin : MonoBehaviour
{
	public TMP_InputField usernameInput;
	public TMP_InputField passwordInput;
	public TMP_InputField confirmPasswordInput;
	public TextMeshProUGUI messageText;
	public AuthUIManager authUIManager;

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
	public void OnLoginButton()
	{
		var request = new LoginWithPlayFabRequest
		{
			Username = usernameInput.text,
			Password = passwordInput.text
		};
		PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
	}

	public void OnRegisterButton()
	{
		if (passwordInput.text != confirmPasswordInput.text)
		{
			messageText.text = "Mật khẩu nhập lại không khớp!";
			passwordInput.text = "";
			confirmPasswordInput.text = "";
			return;
		}

		var request = new RegisterPlayFabUserRequest
		{
			Username = usernameInput.text,
			Password = passwordInput.text,
			RequireBothUsernameAndEmail = false
		};
		PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
	}

	void OnLoginSuccess(LoginResult result)
	{
		messageText.text = "Đăng nhập thành công!";
		passwordInput.text = "";
        // Lấy map đã lưu từ PlayFab UserData

        // Set display name to the username (only once)
        UpdateDisplayName(usernameInput.text);

        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), (userDataResult) =>
        {
            int mapIndex = 1;
            float totalElapsedTime = 0f;

            if (userDataResult.Data != null)
            {
                if (userDataResult.Data.ContainsKey("CurrentMap"))
                    int.TryParse(userDataResult.Data["CurrentMap"].Value, out mapIndex);

                if (userDataResult.Data.ContainsKey("TotalElapsedTime"))
                    float.TryParse(userDataResult.Data["TotalElapsedTime"].Value, out totalElapsedTime);
            }

            GameSession.CurrentMap = mapIndex;
            GameSession.TotalElapsedTime = totalElapsedTime;

            Debug.Log($"[Login] Loaded map: {mapIndex}, total time: {totalElapsedTime:F2}s");

            SceneManager.LoadScene("Menu");
        },
        (error) => {
			Debug.LogError("Lỗi lấy dữ liệu map: " + error.ErrorMessage);
			GameSession.CurrentMap = 1;
			SceneManager.LoadScene("Menu");
		});
	}

    void UpdateDisplayName(string displayName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request,
            result => Debug.Log($"✅ Display name set to: {result.DisplayName}"),
            error => Debug.LogWarning("⚠️ Failed to set display name: " + error.GenerateErrorReport()));
    }

    void OnLoginFailure(PlayFabError error)
	{
		messageText.text = "Đăng nhập thất bại: " + error.ErrorMessage;
		passwordInput.text = "";
	}

	void OnRegisterSuccess(RegisterPlayFabUserResult result)
	{
		messageText.text = "Đăng ký thành công! Hãy đăng nhập.";
		passwordInput.text = "";
		confirmPasswordInput.text = "";

		SaveCurrentMap(1,0);

		if (authUIManager != null)
			authUIManager.ShowLoginUI();
	}

	void OnRegisterFailure(PlayFabError error)
	{
		messageText.text = "Đăng ký thất bại: " + error.ErrorMessage;
		passwordInput.text = "";
		confirmPasswordInput.text = "";
	}

	public void SaveCurrentMap(int mapIndex, float totalElapsedTime)
	{
		Debug.Log("[PlayFabLogin] Gọi SaveCurrentMap với mapIndex = " + mapIndex);
		var request = new PlayFab.ClientModels.UpdateUserDataRequest
		{
			Data = new System.Collections.Generic.Dictionary<string, string>
		{
			{ "CurrentMap", mapIndex.ToString() },
            { "TotalElapsedTime", totalElapsedTime.ToString("F2") }
        }
		};
		PlayFabClientAPI.UpdateUserData(request, result => {
			Debug.Log("[PlayFabLogin] Map đã lưu thành công lên PlayFab: " + mapIndex);
		}, error => {
			Debug.LogError("[PlayFabLogin] Lỗi lưu map lên PlayFab: " + error.ErrorMessage);
		});
	}

	public void ResetAllFields()
	{
		if (usernameInput != null) usernameInput.text = "";
		if (passwordInput != null) passwordInput.text = "";
		if (confirmPasswordInput != null) confirmPasswordInput.text = "";
		if (messageText != null) messageText.text = "";
	}
}