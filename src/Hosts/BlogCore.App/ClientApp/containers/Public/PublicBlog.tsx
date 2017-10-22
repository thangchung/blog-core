import * as React from "react";
import { RouteComponentProps } from "react-router-dom";

export default class PublicBlog extends React.Component<any, {}> {
  render(): JSX.Element {
    //console.log(this.props.match.params);
    return <div className="animated fadeIn">Public blog page.</div>;
  }
}
