import React, {Component} from "react";
import connect from "react-redux/es/connect/connect";
import _ from "lodash";
import {GetSurveyResult} from "../Actions/survey";
import {Link} from "react-router-dom";

class SurveyResults extends Component {

    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const {survey_id, user_id} = this.props.match.params;
        this.props.GetSurveyResult(survey_id, user_id)

    }


    render() {
        console.log(this.props);

        if (!this.props.surveys.survey_results) {
            return <div>Loading...</div>
        }

        if (this.props.surveys.survey_results.message) {
            return (
                <div className="grid">
                    <div className="row">
                        <h1>{this.props.surveys.survey_results.message}</h1>
                    </div>
                </div>
            );
        } else {

            let fields = _.map(this.props.surveys.survey_results.answers, answers => {

                let options = _.map(answers.questionAnswersText, q => {
                    return (
                        <div>
                            <span key={answers.questionNumber * 32}>{q}</span>
                            <br/>
                        </div>
                    )
                });

                return (
                    <li key={answers.questionNumber} className="list-group-item">
                        <span>{answers.questionNumber}. {answers.questionName}</span>
                        <br/>
                        <span>Answers:</span>
                        <br/>
                        {options}
                    </li>
                )
            });

            return (
                <div className="grid">
                    <div className="row">
                        <h1>{this.props.surveys.survey_results.surveyText}</h1>
                    </div>
                    <div className="row">
                        <ul className="container list-group list-group-flush">
                            {fields}
                        </ul>
                    </div>
                </div>
            );
        }
    }
}

function mapStateToProps(state) {
    return {
        surveys: state.surveys
    }
}

export default connect(mapStateToProps, {GetSurveyResult: GetSurveyResult})(SurveyResults);
