import { FlashCardWord } from "./flashCardWord";

export interface FlashCardSet {
    id: string;
    name: string;
    words: FlashCardWord[];
    isFavorite: boolean;
    meta: FlashCardSetMeta;
}

export interface FlashCardSetMeta {
    createdBy: string;
    createdAt: Date;
}