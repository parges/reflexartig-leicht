export interface ApiResponse<T> {
  totalRecords: number;
  items: T[];
}

export interface SelectData<T> {
  display: string;
  value: T;
}

export interface TileGroup {
  title: string;
  tiles: Tile[];
}

export interface Tile {
  title: string;
  icon?: string;
  route: string[];
}
