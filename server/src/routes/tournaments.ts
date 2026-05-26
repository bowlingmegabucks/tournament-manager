import { Router } from 'express';
import * as tournamentController from '../controllers/tournaments.controller';

const router = Router();

router.get('/', tournamentController.list);
router.get('/:id', tournamentController.detail);
router.put('/:id', tournamentController.update);

export default router;
