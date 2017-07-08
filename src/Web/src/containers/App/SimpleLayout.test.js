import React from "react";
import ReactDOM from "react-dom";
import SimpleLayout from "./SimpleLayout";

it("renders without crashing", () => {
  const div = document.createElement("div");
  ReactDOM.render(<AppLayout />, div);
});
