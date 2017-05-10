import { browserHistory } from "react-router";
import createStore from "../redux/configureStore";

// a request helper which reads the access_token from the redux state and passes it in its HTTP request
export default function apiRequest(url, method = "GET") {
  const token = createStore(browserHistory).getState().oidc.user.access_token;
  const headers = new Headers();
  headers.append("Accept", "application/json");
  headers.append("Authorization", `Bearer ${token}`);

  const options = {
    method,
    headers
  };

  console.log("BLOGCORE: HEADER");
  console.log(options);

  return fetch(url, options)
    .then(res => res.json())
    .then(data => ({ data }))
    .catch(error => ({ error }));
}
