import React, {Component} from 'react';
import {connect} from 'react-redux';
import history from './History';

export default function (ComposedComponent) {
    class Authentication extends Component {

        componentWillMount() {
            if (localStorage.getItem("token") === null) {
                history.push('/');
            }
        }

        componentWillUpdate(nextProps) {
            if (localStorage.getItem("token") === null) {
                history.push('/');
            }
        }

        render() {
            return <ComposedComponent {...this.props}/>
        }
    }

    function mapStateToProps(state) {

        return {
            authenticated: state.auth.authenticated
        };
    }

    return connect(mapStateToProps)(Authentication)
}