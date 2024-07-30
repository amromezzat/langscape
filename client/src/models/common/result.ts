export interface IResult<T> {
    messages: ReadonlyArray<string>;
    data: T;
  }