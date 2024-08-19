using System.Collections;
using System.IO;
using UnityEngine;
#if UNITY_2018_4_OR_NEWER
using UnityEngine.Networking;
#endif
using UnityEngine.UI;

namespace Project.Web
{
    public class AstronomicalView : MonoBehaviour
    {
        public string Url;
        public Text status;
        WebViewObject webViewObject;
        public bool IsHaveError = false;

        public void ShowIfOk(string URL)
        {
            Url = URL;
            StartCoroutine(InitSpace());
        }

        private void Show()
        {
            Debug.Log($"IsHaveError = {IsHaveError}");
            if (!IsHaveError)
            {
                webViewObject.SetVisibility(true);
            }
            else
            {
                webViewObject.gameObject.SetActive(false);
            }
        }

        public IEnumerator InitSpace()
        {
            webViewObject = (new GameObject("WebViewObject")).AddComponent<WebViewObject>();
            webViewObject.Init(
                cb: (msg) =>
                {
                    Debug.Log(string.Format("CallFromJS[{0}]", msg));
                    status.text = msg;
                    status.GetComponent<Animation>().Play();
                },
                err: (msg) =>
                {
                    status.text = msg;
                    webViewObject.SetVisibility(false);
                    IsHaveError = true;
                    status.GetComponent<Animation>().Play();
                    Debug.Log(string.Format("CallOnError[{0}] IsHaveError = {1}", msg, IsHaveError));
                },
                httpErr: (msg) =>
                {
                    status.text = msg;
                    webViewObject.SetVisibility(false);
                    IsHaveError = true;
                    status.GetComponent<Animation>().Play();
                    Debug.Log(string.Format("CallOnHttpError[{0}] IsHaveError = {1}", msg, IsHaveError));
                },
                started: (msg) =>
                {
                    Debug.Log(string.Format("CallOnStarted[{0}] IsHaveError = {1}", msg, IsHaveError));
                },
                hooked: (msg) => { Debug.Log(string.Format("CallOnHooked[{0}]", msg)); },
                cookies: (msg) => { Debug.Log(string.Format("CallOnCookies[{0}]", msg)); },
                ld: (msg) =>
                {
                    Debug.Log(string.Format("CallOnLoaded[{0}]", msg));
                    Show();
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_IOS
#if true
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                window.location = 'unity:' + msg;
                            }
                        };
                    }
                ";
#else
                // NOTE: depending on the situation, you might prefer this 'iframe' approach.
                // cf. https://github.com/gree/unity-webview/issues/189
                var js = @"
                    if (!(window.webkit && window.webkit.messageHandlers)) {
                        window.Unity = {
                            call: function(msg) {
                                var iframe = document.createElement('IFRAME');
                                iframe.setAttribute('src', 'unity:' + msg);
                                document.documentElement.appendChild(iframe);
                                iframe.parentNode.removeChild(iframe);
                                iframe = null;
                            }
                        };
                    }
                ";
#endif
#elif UNITY_WEBPLAYER || UNITY_WEBGL
                var js = @"
                    window.Unity = {
                        call:function(msg) {
                            parent.unityWebView.sendMessage('WebViewObject', msg);
                        }
                    };
                ";
#else
                    var js = "";
#endif
                    webViewObject.EvaluateJS(js + @"Unity.call('ua=' + navigator.userAgent)");
                }
                //transparent: false,
                //zoom: true,
                //ua: "custom user agent string",
                //radius: 0,  // rounded corner radius in pixel
                //// android
                //androidForceDarkMode: 0,  // 0: follow system setting, 1: force dark off, 2: force dark on
                //// ios
                //enableWKWebView: true,
                //wkContentMode: 0,  // 0: recommended, 1: mobile, 2: desktop
                //wkAllowsLinkPreview: true,
                //// editor
                //separated: false
            );
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        webViewObject.bitmapRefreshCycle = 1;
#endif
            webViewObject.SetMargins(0, 0, 0, 0);
            webViewObject
                .SetTextZoom(
                    100); // android only. cf. https://stackoverflow.com/questions/21647641/android-webview-set-font-size-system-default/47017410#47017410
            webViewObject.SetVisibility(false);

#if !UNITY_WEBPLAYER && !UNITY_WEBGL
            if (Url.StartsWith("http"))
            {
                webViewObject.LoadURL(Url.Replace(" ", "%20"));
            }
            else
            {
                var exts = new string[]
                {
                    ".jpg",
                    ".js",
                    ".html" // should be last
                };
                foreach (var ext in exts)
                {
                    var url = Url.Replace(".html", ext);
                    var src = Path.Combine(Application.streamingAssetsPath, url);
                    var dst = Path.Combine(Application.temporaryCachePath, url);
                    byte[] result = null;
                    if (src.Contains("://"))
                    {
                        // for Android
#if UNITY_2018_4_OR_NEWER
                        // NOTE: a more complete code that utilizes UnityWebRequest can be found in https://github.com/gree/unity-webview/commit/2a07e82f760a8495aa3a77a23453f384869caba7#diff-4379160fa4c2a287f414c07eb10ee36d
                        var unityWebRequest = UnityWebRequest.Get(src);
                        yield return unityWebRequest.SendWebRequest();
                        result = unityWebRequest.downloadHandler.data;
#else
                    var www = new WWW(src);
                    yield return www;
                    result = www.bytes;
#endif
                    }
                    else
                    {
                        result = File.ReadAllBytes(src);
                    }

                    File.WriteAllBytes(dst, result);
                    if (ext == ".html")
                    {
                        webViewObject.LoadURL("file://" + dst.Replace(" ", "%20"));
                        break;
                    }
                }
            }
#else
        if (Url.StartsWith("http")) {
            webViewObject.LoadURL(Url.Replace(" ", "%20"));
        } else {
            webViewObject.LoadURL("StreamingAssets/" + Url.Replace(" ", "%20"));
        }
#endif
            yield break;
        }
    }
}