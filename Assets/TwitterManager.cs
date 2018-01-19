using System;
using TwitterKit.Unity;
using UnityEngine;

public class TwitterManager : MonoBehaviour {
    private string accessToken;
    private string secret;

    //Twitterクライアントの呼び出し
    public void PostTweet() {
        Debug.Log("tweet");
        var text = "なんかtext";
        var hashtags = "朕猫,test";
        Application.OpenURL($"https://twitter.com/intent/tweet?text={text}&hashtags={hashtags}");
    }

    public void Start() {
        Twitter.Init();

        Debug.Log(Application.persistentDataPath);
        // TwitterAuth();
    }

    public void TwitterAuth() {
        var session = Twitter.Session;
        if (session == null) {
            Twitter.LogIn(LoginComplete, LoginFailure);
        } else {
            LoginComplete(session);
        }
    }

    public void LoginComplete(TwitterSession session) {
        Debug.Log("[Info] : Login success. " + session.authToken);
        accessToken = session.authToken.token;
        secret = session.authToken.secret;
        Debug.Log("accsess::" + accessToken);
        Debug.Log("secret::" + secret);
        TestTweet(session);
    }

    public void LoginFailure(ApiError error) {
        Debug.Log(error);
        Debug.Log("[Error ] : Login faild code =" + error.code + " msg =" + error.message);
    }

    public void TestTweet(TwitterSession session) {
        try {
            ScreenCapture.CaptureScreenshot("Screenshot.png");
            var uri = "file:///" + Application.persistentDataPath + "/Screenshot.png";
            Twitter.Compose(session, uri, "test by", new[] {"#ChinNeko"});
        } catch (Exception e) {
            Debug.Log("error::" + e.Message);
        }
    }
}