import React, {Component} from "react";
import connect from "react-redux/es/connect/connect";
import _ from "lodash";
import {Field, reduxForm} from 'redux-form';
import {getSurveyById, postFinishedSurvey} from "../Actions/survey";

class Survey extends Component {

    constructor(props) {
        super(props);
        this.state = {
            restrictionArray: [],
            question_state_completed: null
        }
    }

    componentDidMount() {

        const {id} = this.props.match.params;
        this.props.getSurveyById(id)

    }

    componentWillReceiveProps(nextProps) {
        let options = nextProps.surveys.survey.survey_options;
        let rArr = [];
        _.map(options, option => {

            let restrictionRequirement = option.question_requirement;
            let restrictionStatement = option.question_statement;

            let restrictionObject = {};

            if (restrictionRequirement === true) {

                restrictionObject.restrictionQuestionNumber = option.question_number;

                restrictionObject.restrictionRequirement = restrictionRequirement;

                restrictionObject.restrictionStatement = restrictionStatement;

                restrictionObject.text = option.question_text;

                restrictionObject.name = `question_answer.${option.question_number}`;

                rArr.push(restrictionObject);

            }
        });

        this.setState({
            restrictionArray: rArr
        })
    }

    handleChange(event, questionNumber) {
        let val = event.target.value;
        let restriction = `${questionNumber}${val}`;
        console.log(restriction);
        _.map(this.state.restrictionArray, arrV => {
            let number = arrV.restrictionStatement.statements[0].charAt(0);
            if (questionNumber == number) {
                if (arrV.restrictionRequirement) {
                    let include = arrV.restrictionStatement.statements.includes(restriction);

                    if (include === true) {
                        _.map(arrV.restrictionStatement.statements, statements => {
                            if (arrV.restrictionStatement.action[0] == "||") {
                                if (restriction == statements) {
                                    this.setState({
                                        question_state_completed: arrV.restrictionQuestionNumber
                                    })
                                }
                            } else {
                                if (restriction == statements) {
                                    this.setState({
                                        question_state_completed: arrV.restrictionQuestionNumber
                                    })
                                }
                            }
                        })
                    } else {
                        this.setState({
                            question_state_completed: null
                        })
                    }
                }
            }

        });
        console.log(this.state.question_state_completed);
    }

    renderTitle() {
        if (!this.props.surveys) {
            return <div>Loading...</div>
        }

        if (this.props.surveys.survey) {
            let surveyTitle = this.props.surveys.survey.survey_name;
            let surveyId = this.props.surveys.survey.survey_id;
            return <h1 data-survey-id={surveyId}>{surveyTitle}</h1>
        }

    }

    onSubmitForm(values) {
        const survey_id = this.props.surveys.survey.survey_id;
        const user_id = localStorage.getItem("id");
        this.props.postFinishedSurvey(values, survey_id, user_id);
    }

    static length(obj) {
        return Object.keys(obj).length;
    }


    render() {

        if (!this.props.surveys) {
            return <div>Loading...</div>
        }

        console.log(this.props);

        let fields;

        if (this.props.surveys.survey) {
            fields = _.map(this.props.surveys.survey.survey_options, surv => {
                let text = `${surv.question_number}. ${surv.question_text}`;
                let questionTypeId = surv.question_type_id;
                let answers = surv.question_answers[0];
                let requirementNumber = surv.question_requirement;

                if (answers != undefined) {

                    let htmlAnswers = _.map(answers, (answer, key) => {
                        let option = `${key}. ${answer}`;
                        if (questionTypeId == 5) {
                            let name = `question_answer.${surv.question_number}.${key}`;
                            return <div key={option}>
                                <Field
                                    name={name}
                                    component="input"
                                    type="checkbox"
                                    value={key}
                                />
                                <div className="text_tag">{option}</div>
                            </div>;
                        }
                        if (questionTypeId == 6) {
                            let name = `question_answer.${surv.question_number}`;
                            return <div key={option}>
                                <Field
                                    name={name}
                                    component="input"
                                    type="radio"
                                    onChange={(event) => this.handleChange(event, surv.question_number)}
                                    value={key}
                                />
                                <div className="text_tag">{option}</div>
                            </div>;
                        }
                    });

                    if (questionTypeId == 5) {
                        return (
                            <div className="form-group row col-12" key={surv.question_number}>
                                <fieldset className="survey">
                                    <label className="col-12 question_text">
                                        {text}
                                    </label>
                                    <div className="col-12">
                                        {htmlAnswers}
                                    </div>
                                </fieldset>
                            </div>
                        )
                    }

                    if (questionTypeId == 6) {
                        return (
                            <div className="form-group row col-12" key={surv.question_number}>
                                <fieldset className="survey">
                                    <label className="col-12 question_text">
                                        {text}
                                    </label>
                                    <div className="col-12">
                                        {htmlAnswers}
                                    </div>
                                </fieldset>
                            </div>
                        )
                    }

                } else {

                    let name = `question_answer.${surv.question_number}`;

                    if (questionTypeId == 4) {
                        return (
                            <div className="form-group row col-12" key={surv.question_number}>
                                <fieldset className="survey">
                                    <label className="col-12 question_text">
                                        {text}
                                    </label>
                                    <div className="col-12">
                                        <Field className="form-control" placeholder={text} name={name} component="input"
                                               type="number"/>
                                    </div>
                                </fieldset>
                            </div>
                        )
                    }

                    if (questionTypeId == 7) {

                        console.log(this.state);
                        return (
                            <div className="form-group row col-12" key={surv.question_number}>
                                {
                                    this.state.question_state_completed == surv.question_number ?
                                        <fieldset className="survey">
                                            <label className="col-12 question_text">
                                                {text}
                                            </label>
                                            <div className="col-12">
                                                <Field className="form-control" placeholder={text} name={name}
                                                       component="input"
                                                       type="text"/>
                                            </div>
                                        </fieldset>
                                        :

                                        <fieldset className="survey">

                                        </fieldset>
                                }
                            </div>
                        )
                    }
                }
            })
        } else {
            fields = <div>Loading...</div>
        }

        const {handleSubmit} = this.props;

        return (
            <div className="grid">
                <div className="row">
                    {this.renderTitle()}
                </div>
                <div className="row">
                    <form className="col-12" onSubmit={handleSubmit(this.onSubmitForm.bind(this))}>
                        {fields}
                        <button type="submit" className="btn btn-primary form_button">Send survey</button>
                    </form>
                </div>
            </div>
        )
    }
}

function mapStateToProps(state) {
    return {
        surveys: state.surveys
    }
}

export default reduxForm({
    form: 'Survey'
})(
    connect(mapStateToProps, {
        getSurveyById: getSurveyById,
        postFinishedSurvey: postFinishedSurvey
    })(Survey)
);