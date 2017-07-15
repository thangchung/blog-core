import React, { Component } from 'react';
import PropTypes from 'prop-types';
import { connect } from 'react-redux';

export class Restricted extends Component {
    static propTypes = {
        location: PropTypes.object
    }

    componentWillMount() {
        this.checkAuthentication(this.props);
    }

    componentWillReceiveProps(nextProps) {
        if (nextProps.location !== this.props.location) {
            this.checkAuthentication(nextProps);
        }
    }

    checkAuthentication(params) {
        const { authParams, location } = params;
        // userCheck(authParams, location && location.pathname);
    }

    // render the child even though the AUTH_CHECK isn't finished (optimistic rendering)
    render() {
        const { children, ...rest } = this.props;
        return React.cloneElement(children, rest);
    }
}

export default connect(null, null)(Restricted);