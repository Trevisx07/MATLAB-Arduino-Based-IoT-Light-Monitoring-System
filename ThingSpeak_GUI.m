%% Clear command window and variables
clc; clear; close all;

%% Define ThingSpeak Channel ID and Read API Key
ThingSpeak_channel = '2883041';          % Set Your ThingSpeak Channel
Read_API_Key = '9E4WL1H6VDC2HQHM';       % Set Your Read API Key

%% Create GUI Figure
fig = figure('Name', 'ThingSpeak Data Visualization', ...
             'NumberTitle', 'off', ...
             'Position', [100 100 600 400]);

% Create Axes for Plot
ax = axes('Parent', fig, ...
          'Position', [0.1 0.3 0.8 0.6]); % Adjusted position
grid on;
title(ax, 'Real-Time ThingSpeak Data');
xlabel(ax, 'Time');
ylabel(ax, 'Sensor Value');

% Create Text Display for Latest Data
dataText = uicontrol('Style', 'text', ...
                     'Parent', fig, ...
                     'Position', [250 50 100 40], ...
                     'FontSize', 14, ...
                     'FontWeight', 'bold', ...
                     'BackgroundColor', 'white', ...
                     'String', 'Waiting...');

% Create Refresh Button
refreshButton = uicontrol('Style', 'pushbutton', ...
                          'Parent', fig, ...
                          'Position', [250 10 100 30], ...
                          'String', 'Refresh Data', ...
                          'FontSize', 12, ...
                          'Callback', @(src, event) updatePlot(ax, dataText, ThingSpeak_channel, Read_API_Key));

%% Function to Fetch and Update Plot
function updatePlot(ax, dataText, ThingSpeak_channel, Read_API_Key)
    % Fetch Data from ThingSpeak
    url = sprintf('https://api.thingspeak.com/channels/%s/fields/1.json?api_key=%s&results=10', ...
                  ThingSpeak_channel, Read_API_Key);
    
    data = webread(url); % Read JSON data
    
    % Extract Data
    timestamps = datetime({data.feeds.created_at}, 'InputFormat', 'yyyy-MM-dd''T''HH:mm:ss''Z''', 'TimeZone', 'UTC');
    values = str2double({data.feeds.field1}); % Convert to numbers
    
    % Plot Data
    plot(ax, timestamps, values, '-o', 'LineWidth', 2);
    title(ax, 'Real-Time ThingSpeak Data');
    xlabel(ax, 'Time');
    ylabel(ax, 'Sensor Value');
    grid(ax, 'on');
    
    % Update Latest Value Display
    latestValue = values(end);
    set(dataText, 'String', sprintf('Latest: %.2f', latestValue));
end

%% Initial Plot Update
updatePlot(ax, dataText, ThingSpeak_channel, Read_API_Key);
