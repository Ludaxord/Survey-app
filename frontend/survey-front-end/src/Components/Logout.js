import React, {Component} from "react";

import history from "../Helpers/History";

class Logout extends Component {

    static logout() {
        localStorage.clear();
        setTimeout(function () {
            history.push("/")
        }, 2000)
    }

    componentDidMount() {
        Logout.logout()
    }

    render() {
        return (
            <div>
                Thank you for using Survey App, see you later!
            </div>
        )
    }
}

export default Logout;