import React, {Component} from "react";
import * as actions from "../Actions/auth.js"
import {connect} from "react-redux";
import {withRouter} from "react-router";
import {Field, reduxForm} from "redux-form";

class Register extends Component {

    formSubmit({Email, Password, FirstName, LastName}) {
        this.props.register({Email, Password, FirstName, LastName})
    }

    render() {
        const {handleSubmit, fields: {Email, Password, FirstName, LastName}} = this.props;
        return (
            <div className="grid">
                <div className="row">
                    <h1>Register</h1>
                </div>
                <div className="row">
                    <form onSubmit={handleSubmit(this.formSubmit.bind(this))}>
                        <div className="form-group">
                            <label>Email:</label>
                            <Field className="form-control" {...Email} name="Email" placeholder="Email" type="text" component="input"/>
                            <label>Password:</label>
                            <Field className="form-control" {...Password} placeholder="Password" name="Password" type="password"
                                   component="input"/>
                            <label>First Name:</label>
                            <Field className="form-control" {...FirstName} placeholder="First Name" name="FirstName" type="text"
                                   component="input"/>
                            <label>Last Name:</label>
                            <Field className="form-control" {...LastName} placeholder="Last Name" name="LastName" type="text"
                                   component="input"/>
                            <button action="submit" className="btn btn-primary form_auth">Register</button>
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

Register = reduxForm({form: 'Register', fields: ['email', 'password', 'firstName', 'lastName']})(Register);

export default withRouter(connect(mapStateToProps, actions)(Register));