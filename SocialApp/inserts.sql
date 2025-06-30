-- used to fill the database with initial mock dat
use [SocialAppMigrated]
-- Users

INSERT INTO Users (username, password, photo_url) VALUES
('fit_alice', 'alice1234', NULL),
('diet_bob', 'bob_password', NULL),
('carol_runs', 'qwerty!', NULL),
('luka_lifts', 'pa$$word', NULL),
('eve_nutrition', 'eve2025', NULL),
('frank_fitness', 'frankly123', NULL),
('grace_gym', 'graceful!', NULL),
('heidi_healthy', 'heidiPass1', NULL),
('ivan_ivanov', 'ivanStrong', NULL),
('lbj_fan_23', 'lakersin5', NULL);

-- Groups

INSERT INTO Groups (name, description) VALUES
('Calorie Counters', 'A community for tracking calories and healthy eating habits.'),
('Tech Geeks', 'Discuss the latest gadgets, software, and tech trends.'),
('Book Lovers Club', 'Join us for book discussions and recommendations.'),
('Fitness Freaks', 'Motivation and advice for your fitness journey.'),
('Culinary Adventures', 'Explore recipes, cooking tips, and foodie stories.');

select *
from Reactions
select * from GroupUsers
where user_id = 1
select * from Groups
-- Posts
INSERT INTO Posts (title, content, user_id, group_id, visibility, tag) VALUES
('Morning Yoga Routines', 'Start your day with these energizing yoga poses.', 4, null, 3, 3),
('HIIT for Beginners', 'A simple high-intensity interval training guide for all levels.', 9, null, 3, 3),
('Home Workout Essentials', 'Must-have gear for effective home workouts.', 4, null, 3, 3),
('Nutrition Basics', 'Understanding macros and micros for a balanced diet.', 9, null, 3, 3),
('Running for Beginners', 'Tips to start running injury-free.', 4, null, 3, 3),
('Yoga and Meditation', 'How mindfulness and movement boost your health.', 9, null, 3, 3),
('Healthy Smoothies', 'Delicious recipes for nutritious drinks.', 10, null, 3, 4),
('Bodyweight Exercises', 'Stay fit anywhere with these no-equipment moves.', 4, null, 3, 3),
('Stretching Routines', 'Improve flexibility and prevent injuries.', 9, null, 3, 3),
('Cardio Workouts', 'Boost your heart health with these routines.', 4, null, 3, 3),
('Meal Prep for Fitness', 'Easy meal prep ideas to support your goals.', 5, null, 3, 4),
('Best Plant-Based Proteins', 'Top sources of protein for vegetarians and vegans.', 5, null, 3, 4),
('Fitness Transformation Stories', 'Real journeys, real results.', 6, null, 3, 3),
('Motivational Fitness Quotes', 'Stay inspired on your fitness journey.', 6, null, 3, 3),
('Quick Workouts for Busy Days', 'Stay active even on your tightest schedule.', 7, null, 3, 3),
('Healthy Snack Ideas', 'Nutritious snacks to fuel your workouts.', 5, null, 3, 4),
('Weekly Workout Challenge', 'Join the 7-day fitness challenge!', 8, null, 3, 3),
('Hydration Tips', 'Why staying hydrated is key for performance.', 9, 4, 4, 3),
('Meal Planning 101', 'How to plan your week for healthy eating.', 5, 5, 4, 4),
('Strength Training Basics', 'Beginner’s guide to lifting weights.', 4, 4, 4, 3),
('Post-Workout Recovery', 'Best practices for muscle recovery.', 6, 4, 4, 3),
('Low-Carb Recipes', 'Tasty dishes for a low-carb lifestyle.', 5, 5, 4, 4),
('Fitness Myths Busted', 'Debunking common fitness misconceptions.', 7, 4, 4, 3),
('Outdoor Workouts', 'Take your fitness routine outside.', 8, 4, 4, 3),
('Importance of Rest Days', 'Why rest is crucial for progress.', 6, 4, 4, 3),
('Gym Motivation Playlist', 'Top tracks to power your workouts.', 7, 4, 4, 3),
('Healthy Breakfast Ideas', 'Start your day with these nutritious options.', 5, 5, 4, 4),
('Pre-Workout Meals', 'What to eat before you exercise.', 5, 5, 4, 4),
('Fitness Apps Review', 'Best apps to track your progress.', 7, 4, 4, 3),
('Staying Consistent', 'How to build lasting fitness habits.', 6, 4, 4, 3),
('Vegan Meal Prep', 'Easy plant-based meals for fitness.', 5, 5, 4, 4),
('Beginner’s Guide to Pilates', 'Why Pilates is great for core strength.', 8, 4, 4, 3),
('Supplements 101', 'What you need to know about fitness supplements.', 9, 4, 4, 3),
('Group Fitness Benefits', 'Why working out together works.', 8, 4, 4, 3),
('Healthy Eating on a Budget', 'Affordable ways to eat well.', 5, 5, 4, 4),
('Fitness Progress Tracking', 'Simple ways to measure your results.', 6, null, 2, 3),
('Sunset over the Mountains', 'Captured this stunning view during my hike last weekend.', 1, null, 2, 1),
('Best Budget Laptops in 2025', 'My top picks for affordable laptops.', 2, null, 2, 1),
('Top 5 Mystery Novels to Read', 'Suspenseful books to keep you hooked!', 3, null, 2, 2),
('Night Sky Photography Tips', 'How to capture stars like a pro.', 6, null, 2, 0),
('Upcoming Smartphone Features', 'What to expect from next-gen phones.', 7, null, 2, 1),
('Historical Fiction Gems', 'Books that transport you to another era.', 8, null, 2, 2),
('Macro Photography Techniques', 'Get the details in your close-ups.', 1, null, 2, 1),
('AI in Everyday Life', 'How AI is shaping our future.', 2, null, 2, 1),
('Poetry Recommendations', 'Beautiful poems to soothe the soul.', 3, null, 2, 2),
('Long Exposure Photography', 'Create magical light trails.', 6, null, 2, 1),
('Latest Software Updates', 'What’s new this month.', 7, 2, 4, 1),
('Classic Novels Everyone Should Read', 'Timeless stories.', 8, 3, 4, 2),
('Black and White Photography', 'Mastering contrast and shadows.', 6, 1, 4, 1),
('Gadget Reviews', 'Honest opinions on the latest tech.', 7, 2, 4, 1),
('Writing Tips', 'Improve your writing style easily.', 8, 3, 4, 2);

-- Comments
INSERT INTO Comments (user_id, post_id, content) VALUES
(2, 1, 'Yoga really helps me start the day right!'),
(3, 2, 'HIIT is intense but so effective for burning calories.'),
(4, 3, 'Thanks for the home gear tips, makes working out easier.'),
(5, 4, 'Macros and micros always confused me, this post helped!'),
(6, 5, 'Started running last month, seeing real progress.'),
(7, 6, 'Yoga and meditation keep me balanced every day.'),
(8, 7, 'Smoothie recipes are delicious and easy to make.'),
(9, 8, 'Bodyweight moves are perfect for travel workouts.'),
(10, 9, 'Stretching routines help prevent injuries and improve performance.'),
(1, 10, 'Cardio is crucial for endurance and weight loss.'),
(2, 11, 'Meal prep saves me so much time during the week!'),
(3, 12, 'Plant-based proteins are a game changer for my diet.'),
(4, 13, 'Transformation stories keep me motivated to push harder.'),
(5, 14, 'Love these motivational quotes, needed the boost!'),
(6, 15, 'Quick workouts fit perfectly into my busy schedule.'),
(7, 16, 'Healthy snacks keep me energized throughout the day.'),
(8, 17, 'I’m in for the weekly challenge, let’s go!'),
(9, 18, 'Hydration tips are so important, thanks for sharing.'),
(10, 19, 'Meal planning helps me avoid unhealthy choices.'),
(1, 20, 'Strength training made me feel so much stronger.'),
(2, 21, 'Post-workout recovery is key, thanks for the advice.'),
(3, 22, 'Low-carb recipes are tasty and filling!'),
(4, 23, 'Glad you’re busting these fitness myths.'),
(5, 24, 'Outdoor workouts make exercise more fun.'),
(6, 25, 'Rest days are essential for progress, totally agree.'),
(7, 26, 'Great playlist, music keeps me moving!'),
(8, 27, 'Breakfast ideas are so helpful, thanks!'),
(9, 28, 'Pre-workout meals really make a difference.'),
(10, 29, 'Tracking my progress keeps me accountable.'),
(1, 30, 'Staying consistent is the hardest part, but worth it.'),
(2, 31, 'Vegan meal prep is easier than I thought!'),
(3, 32, 'Pilates has really improved my core strength.'),
(4, 33, 'Supplements can be confusing, thanks for clarifying.'),
(5, 34, 'Group workouts are so much more motivating.'),
(6, 35, 'Eating healthy on a budget is possible with these tips.'),
(7, 36, 'Tracking progress keeps me focused on my goals.'),
(8, 37, 'Wow, that sunset looks amazing! Where exactly was this taken?'),
(9, 38, 'Thanks for the laptop recommendations, very helpful!'),
(10, 39, 'I love mystery novels, will definitely check these out.'),
(1, 40, 'Night sky shots are tricky, thanks for the tips.'),
(2, 41, 'Excited about these new phone features!'),
(3, 42, 'Historical fiction is my favorite genre, nice list!'),
(4, 43, 'Macro shots require patience, great tips!'),
(5, 44, 'AI is everywhere nowadays, fascinating topic.'),
(6, 45, 'Poetry always soothes my soul, thanks for sharing.'),
(7, 46, 'Long exposure shots always amaze me.'),
(8, 47, 'Keeping up with software updates is so important.'),
(9, 48, 'Classic novels are timeless, love your list.'),
(10, 49, 'Black and white photography is so artistic.'),
(1, 50, 'Honest gadget reviews save me money, thanks!'),
(2, 51, 'Writing tips always welcome, need to improve.');

-- Reactions
-- ReactionType: Like = 1, Love = 2, Laugh = 3, Anger = 4
INSERT INTO Reactions (user_id, post_id, reaction_type) VALUES
(1, 1, 1),
(2, 2, 2),
(3, 3, 3),
(4, 4, 1),
(5, 5, 2),
(6, 6, 3),
(7, 7, 1),
(8, 8, 2),
(9, 9, 3),
(10, 10, 1),
(1, 11, 2),
(2, 12, 4),
(3, 13, 1),
(4, 14, 2),
(5, 15, 3),
(6, 16, 1),
(7, 17, 2),
(8, 18, 3),
(9, 19, 1),
(10, 20, 2),
(1, 21, 4),
(2, 22, 1),
(3, 23, 2),
(4, 24, 3),
(5, 25, 1),
(6, 26, 2),
(7, 27, 3),
(8, 28, 1),
(9, 29, 2),
(10, 30, 3),
(1, 31, 1),
(2, 32, 2),
(3, 33, 3),
(4, 34, 1),
(5, 35, 2),
(6, 36, 3),
(7, 37, 1),
(8, 38, 2),
(9, 39, 3),
(10, 40, 1),
(1, 41, 2),
(2, 42, 3),
(3, 43, 1),
(4, 44, 2),
(5, 45, 3),
(6, 46, 1),
(7, 47, 2),
(8, 48, 3),
(9, 49, 1),
(10, 50, 2);

-- GroupUsers: Assign users to groups
INSERT INTO GroupUsers (user_id, group_id) VALUES
(1, 1), (1, 2), (1, 4),
(2, 1), (2, 2),
(3, 3), (3, 4), 
(4, 1), (4, 4),
(5, 2), (5, 4), (5, 5),
(6, 1), (6, 2), (6, 4),
(7, 2), (7, 4),
(8, 3), (8, 4), 
(9, 4),
(10, 4), (10, 5);

INSERT INTO UserFollowers (user_id, follower_id) VALUES
(1, 2), (1, 3), (1, 4), (1, 5), (1, 8), (1, 10),
(2, 1),
(3, 2), (3, 5), (3, 10),
(4, 1), (4, 6), (4, 7), (4, 8),
(5, 3), (5, 7),
(6, 1), (6, 5),
(7, 2), (7, 4), (7, 10),
(8, 6), (8, 7), (8, 9),
(9, 1), (9, 2), (9, 4), (9, 10), 
(10, 5);


-- Test insertions
SELECT * FROM Users WHERE username = 'fit_alice';

SELECT * FROM Groups WHERE name = 'Calorie Counters';

SELECT * FROM Posts WHERE title = 'Morning Yoga Routines';

SELECT * FROM Comments WHERE user_id = 1;

SELECT * FROM Reactions WHERE reaction_type = 2;

SELECT * FROM GroupUsers WHERE group_id = 1;

SELECT * FROM UserFollowers WHERE user_id = 1;
