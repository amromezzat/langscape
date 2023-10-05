import { FlashCardWord } from "./flashCardWord";

export interface FlashCardSet {
    id: string;
    name: string;
    words: FlashCardWord[];
}