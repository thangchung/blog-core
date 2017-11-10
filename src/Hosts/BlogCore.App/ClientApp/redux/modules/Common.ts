import { Reducer } from 'redux';

export interface CommonState {
  redirectTo: string;
}

const REDIRECT_TO = 'REDIRECT_TO';

export type RedirectToAction = {
  type: typeof REDIRECT_TO;
  to: string | null;
};

export const actionCreators = {
  redirectTo: (to: string) => <RedirectToAction>{ type: REDIRECT_TO, to }
};

export type KnownAction = RedirectToAction;

export const reducer: Reducer<CommonState> = (
  state: CommonState,
  action: KnownAction
) => {
  switch (action.type) {
    case REDIRECT_TO:
      return {
        ...state,
        to: action.to
      };
    default:
    // const exhaustiveCheck: never = action;
    // if (typeof exhaustiveCheck != "undefined") break;
  }

  return (
    state || {
      to: null
    }
  );
};
