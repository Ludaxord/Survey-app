import React, {Component} from "react";
import {Link} from "react-router-dom";

class Menu extends Component {

    static checkIfLogged(token) {

        if (token == null) {
            return [
                <li key={1} className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
                </li>,
                <li key={2} className="nav-item">
                    <Link className="nav-link" to="/login">Login</Link>
                </li>,
                <li key={3} className="nav-item">
                    <Link className="nav-link" to="/register">Register</Link>
                </li>,
                <li key={4} className="nav-item">
                    <Link className="nav-link disabled" onClick={(e) => e.preventDefault()} to="/survey_list">Survey
                        List</Link>
                </li>,
                <li key={5} className="nav-item">
                    <Link className="nav-link disabled" onClick={(e) => e.preventDefault()} to="/logout">Logout</Link>
                </li>
            ]
        } else {
            return [
                <li key={1} className="nav-item">
                    <Link className="nav-link" to="/">Home</Link>
                </li>,
                <li key={2} className="nav-item">
                    <Link className="nav-link disabled" onClick={(e) => e.preventDefault()} to="/login">Login</Link>
                </li>,
                <li key={3} className="nav-item">
                    <Link className="nav-link disabled" onClick={(e) => e.preventDefault()}
                          to="/register">Register</Link>
                </li>,
                <li key={4} className="nav-item">
                    <Link className="nav-link" to="/survey_list">Survey List</Link>
                </li>,
                <li key={5} className="nav-item">
                    <Link className="nav-link" to="/logout">Logout</Link>
                </li>
            ]
        }
    }


    render() {

        const token = localStorage.getItem("token");

        return (
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container">
                    <Link className="navbar-brand" to="/">Survey App</Link>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav"
                            aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNav">
                        <ul className="navbar-nav">

                            {Menu.checkIfLogged(token)}
                        </ul>
                    </div>
                </div>
            </nav>
        )
    }


}

export default Menu;