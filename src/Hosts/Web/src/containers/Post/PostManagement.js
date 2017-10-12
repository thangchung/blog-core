import React from "react";
import { connect } from "react-redux";

class PostManagement extends React.Component {
  render() {
    return (
      <div>
        Post management page.
      </div>
    );
  }
}

export default connect(null, null)(PostManagement);
