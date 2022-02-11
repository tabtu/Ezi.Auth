const tokenKey = "lastToken";

export function UpdateToken(appid, token, expiry) {}

export function GetLastAccessToken() {}

// TODO: Token have to save into Session.
export function ReadFile(fileName) {
  const reader = new FileReader();
  reader.onload = function fileReadCompleted() {
    console.log(reader.result);
  };
  reader.readAsText(this.files[0]);
}

export function SaveFile(content) {
  //   window.localStorage.setItem(tokenKey, content);
  window.localSession();
}
