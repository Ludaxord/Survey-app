import React, {Component} from "react";
import {connect} from "react-redux";
import {getMainPageMessage} from "../Actions/auth";

class MainPage extends Component {

    componentWillMount() {
        this.props.getMainPageMessage();
    }

    render() {
        if (!this.props.messages) {
            return <div>Loading...</div>
        }

        const mainPageMessage = this.props.messages.home_page_message;

        return (
            <div className="grid">
                <div className="row">
                    <h1>Home</h1>
                </div>
                <div className="row">
                    <span>{mainPageMessage}</span>
                </div>
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        messages: state.messages
    }
}

export default connect(mapStateToProps,{getMainPageMessage: getMainPageMessage})(MainPage);