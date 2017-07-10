import React, { Component } from "react";
import { connect } from "react-redux";
import { Link } from "react-router";
import { Button } from "reactstrap";
import userManager from "../../utils/userManager";
import BlogHeader from "../../components/BlogHeader.js";

class Home extends Component {
  render() {
    return (
      <div>
        <BlogHeader />
        <div className="blog-post">
          <h2 className="blog-post-title">Sample blog post</h2>
          <p className="blog-post-meta">
            January 1, 2014 by <a href="#">Mark</a>
          </p>

          <p>
            This blog post shows a few different types of content that's
            supported and styled with Bootstrap. Basic typography, images, and
            code are all supported.
          </p>
          <hr />
          <p>
            Cum sociis natoque penatibus et magnis
            &nbsp;<a href="#">dis parturient montes</a>, nascetur ridiculus mus.
            Aenean eu leo quam. Pellentesque ornare sem lacinia quam venenatis
            vestibulum. Sed posuere consectetur est at lobortis. Cras mattis
            consectetur purus sit amet fermentum.
          </p>
          <blockquote>
            <p>
              Curabitur blandit tempus porttitor.
              &nbsp;<strong>Nullam quis risus eget urna mollis</strong> ornare
              vel eu leo. Nullam id dolor id nibh ultricies vehicula ut id elit.
            </p>
          </blockquote>
          <p>
            Etiam porta <em>sem malesuada magna</em> mollis euismod. Cras mattis
            consectetur purus sit amet fermentum. Aenean lacinia bibendum nulla
            sed consectetur.
          </p>
          <h2>Heading</h2>
          <p>
            Vivamus sagittis lacus vel augue laoreet rutrum faucibus dolor
            auctor. Duis mollis, est non commodo luctus, nisi erat porttitor
            ligula, eget lacinia odio sem nec elit. Morbi leo risus, porta ac
            consectetur ac, vestibulum at eros.
          </p>
          <h3>Sub-heading</h3>
          <p>
            Cum sociis natoque penatibus et magnis dis parturient montes,
            nascetur ridiculus mus.
          </p>
          <pre><code>Example code block</code></pre>
          <p>
            Aenean lacinia bibendum nulla sed consectetur. Etiam porta sem
            malesuada magna mollis euismod. Fusce dapibus, tellus ac cursus
            commodo, tortor mauris condimentum nibh, ut fermentum massa.
          </p>
          <h3>Sub-heading</h3>
          <p>
            Cum sociis natoque penatibus et magnis dis parturient montes,
            nascetur ridiculus mus. Aenean lacinia bibendum nulla sed
            consectetur. Etiam porta sem malesuada magna mollis euismod. Fusce
            dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh,
            ut fermentum massa justo sit amet risus.
          </p>
          <ul>
            <li>
              Praesent commodo cursus magna, vel scelerisque nisl consectetur
              et.
            </li>
            <li>Donec id elit non mi porta gravida at eget metus.</li>
            <li>Nulla vitae elit libero, a pharetra augue.</li>
          </ul>
          <p>
            Donec ullamcorper nulla non metus auctor fringilla. Nulla vitae elit
            libero, a pharetra augue.
          </p>
          <ol>
            <li>Vestibulum id ligula porta felis euismod semper.</li>
            <li>
              Cum sociis natoque penatibus et magnis dis parturient montes,
              nascetur ridiculus mus.
            </li>
            <li>
              Maecenas sed diam eget risus varius blandit sit amet non magna.
            </li>
          </ol>
          <p>
            Cras mattis consectetur purus sit amet fermentum. Sed posuere
            consectetur est at lobortis.
          </p>
        </div>
      </div>
    );
  }
}

export default connect(null, null)(Home);
