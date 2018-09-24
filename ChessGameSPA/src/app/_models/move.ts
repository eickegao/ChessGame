import { Game } from './game';
import { User } from './user';
import { Piece } from './piece';

export class Move {
    id: number;
    gameId: number;
    pieceId: number;
    userId: number;
    startX: number;
    startY: number;
    endX: number;
    endY: number;
    discriminator: string;
    isWhite: boolean;

    /**
     *
     */
    constructor() { }

    addMove(move: any) {
        this.id = move.id;
    }
}
