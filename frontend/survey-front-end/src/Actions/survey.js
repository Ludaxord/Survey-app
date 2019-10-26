import axios from "axios";
import history from '../Helpers/History';
import _ from "lodash";
import {GET_SURVEY, GET_SURVEY_RESULTS, GET_TITLES, POST_SURVEY} from "../Types/types";

const url = "https://localhost:5001/api";

export function getTitles() {
    return function (dispatch) {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("id");
        const config = {
            headers: {'Authorization': "bearer " + token}
        };

        axios.get(`${url}/survey/gettitles/${userId}`, config).then(response => {
            const titles = response.data;
            dispatch({
                type: GET_TITLES,
                payload: titles
            })
        })

    }
}

export function GetSurveyResult(survey_id, user_id) {
    return function (dispatch) {

        const token = localStorage.getItem("token");
        const config = {
            headers: {'Authorization': "bearer " + token}
        };

        axios.get(`${url}/survey/getsurveyresult/${survey_id}/${user_id}`, config)
            .then(response => {
                const survey = response.data;
                dispatch({
                    type: GET_SURVEY_RESULTS,
                    payload: survey
                })
            })
    }
}

export function getSurveyById(survey_id) {
    return function (dispatch) {

        const token = localStorage.getItem("token");
        const config = {
            headers: {'Authorization': "bearer " + token}
        };

        axios.get(`${url}/survey/getbysurveyid/${survey_id}`, config)
            .then(response => {
                const survey = response.data;
                dispatch({
                    type: GET_SURVEY,
                    payload: survey
                })
            })
    }
}

export function postFinishedSurvey(values, survey_id, user_id) {
    return function (dispatch) {
        const token = localStorage.getItem("token");
        const config = {
            headers: {'Authorization': "bearer " + token}
        };
        let JSON = {};
        JSON.Id = survey_id;
        JSON.UserId = parseInt(user_id);
        let Questions = [];

        _.map(values.question_answer, (value, key) => {
            if (key != 0) {
                let AnswerJSON = {};
                AnswerJSON.question_number = key;
                AnswerJSON.question_answer = value;
                Questions.push(AnswerJSON)
            }
        });

        JSON.Questions = Questions;

        axios.post(`${url}/survey/PostFinishedSurvey/${survey_id}/${user_id}`, JSON, config)
            .then(response => {
                const message = response.data;
                dispatch({
                    type: POST_SURVEY,
                    payload: message
                });
                history.push("/Survey_list");
            });

    }

}