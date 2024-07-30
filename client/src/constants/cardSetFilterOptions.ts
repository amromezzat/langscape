export enum setFilterType {
  none,
  all,
  created,
  favorites
}

export const setFilterOptions = new Map<setFilterType, string>([
  [setFilterType.all, 'All'],
  [setFilterType.created, 'Created'],
  [setFilterType.favorites, 'Favorites']
]);

export const setServerFilterOptions = new Map<setFilterType, string>([
  [setFilterType.created, 'owned'],
  [setFilterType.favorites, 'favorites']
]);