import {GET_SURVEY, GET_SURVEY_RESULTS, GET_TITLES, POST_SURVEY} from "../Types/types";

export default function (state = {}, action) {
    switch (action.type) {
        case GET_SURVEY_RESULTS:
            return {...state, survey_results: action.payload};
        case GET_SURVEY:
            return {...state, survey: action.payload};
        case GET_TITLES:
            return {...state, titles: action.payload};
        case POST_SURVEY:
            return {...state, post_message: action.payload};
    }
    return state;
}