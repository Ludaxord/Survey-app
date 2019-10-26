import React, {Component} from "react";
import * as actions from "../Actions/auth.js"
import {connect} from "react-redux";
import {withRouter} from "react-router";
import {Field, reduxForm} from "redux-form";

class Login extends Component {

    formSubmit({Email, Password}) {
        this.props.login({Email, Password})
    }

    render() {
        const {handleSubmit, fields: {Email, Password}} = this.props;
        return (
            <div className="grid">
                <div className="row">
                    <h1>Login</h1>
                </div>
                <div className="row">
                    <form onSubmit={handleSubmit(this.formSubmit.bind(this))}>
                        <div className="form-group">
                            <label>Email:</label>
                            <Field className="form-control" {...Email} placeholder="Email" name="Email" type="text" component="input"/>
                            <label>Password:</label>
                            <Field className="form-control" {...Password} placeholder="Password" name="Password" type="password"
                                   component="input"/>
                            <button action="submit" className="btn btn-primary form_auth">Login</button>
                        </div>
                    </form>
                </div>
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        error: state.auth.error
    }
}

Login = reduxForm({form: 'Login', fields: ['email', 'password']})(Login);

export default withRouter(connect(mapStateToProps, actions)(Login));