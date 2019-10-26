import React, {Component} from "react";
import {connect} from "react-redux";
import _ from "lodash";
import {getMessage} from "../Actions/auth";
import {getTitles} from "../Actions/survey";
import {Link} from "react-router-dom";

class SurveyList extends Component {

    componentWillMount() {
        this.props.getMessage();
        this.props.getTitles();
    }

    showTitles() {
        if (!this.props.surveys) {
            return <div>Loading...</div>
        }

        return _.map(this.props.surveys.titles, titles => {

            const title = titles.survey_name;
            const id = titles.survey_id;
            const userId = localStorage.getItem("id");

            return (
                <li key={id} data-title-id={id} className="list-group-item">
                    <Link to={`/survey/${id}`}>
                        {title}
                    </Link>
                    <Link style={{float:"Right"}} className="btn btn-primary" to={`/survey_result/${id}/${userId}`}>
                          Show Results
                    </Link>
                </li>
            )


        });
    }

    render() {

        if (!this.props.messages) {
            return <div>Loading...</div>
        }

        const welcomeMessage = this.props.messages.welcome_message;

        if (this.props.surveys.post_message) {
            if (this.props.surveys.post_message.message != undefined) {
                alert(this.props.surveys.post_message.message);
                this.props.surveys.post_message = null;
            }
        }

        return (
            <div className="grid">
                <div className="row">
                    <h1>Survey List</h1>
                </div>
                <div className="row">
                    <span>{welcomeMessage}</span>
                </div>
                <div className="row">
                    <ul className="container list-group list-group-flush">
                        {this.showTitles()}
                    </ul>
                </div>
            </div>
        );

    }
}

function mapStateToProps(state) {
    return {
        messages: state.messages,
        surveys: state.surveys
    }
}

export default connect(mapStateToProps, {
    getMessage, getTitles: getTitles
})(SurveyList);