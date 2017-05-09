export default store => next => action => {
  console.info("Action type:", action.type);
  console.info("Action payload:", action.payload);
  console.info("State before:", store.getState());
  next(action);
  console.info("State after:", store.getState());
};
